using System;
using System.Net;
using System.Net.Http;
using NLog;
using System.Security.Authentication;
using System.Diagnostics;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MySpector.Objects
{
    public class HttpGrabber : IGrabber
    {
        const string SWITCH_HTTP2_SUPPORT = "System.Net.Http.SocketsHttpHandler.Http2Support";
        const string SWITCH_USE_SOCKET_HTTP_HANDLER = "System.Net.Http.UseSocketsHttpHandler";

        static Logger _log = LogManager.GetCurrentClassLogger();

        public HttpGrabber()
        {
        }

        public static HttpGrabber Create()
        {
            return new HttpGrabber();
        }

        public GrabResponse Grab(IGrabTarget target)
        {
            GrabResponse ret;
            if (target == null)
            {
                _log.Error($"No target is set !");
            }
            if (target.TargetType != TargetType.HTTP)
            {
                return null;
            }
            try
            {
                var watch = new Stopwatch();
                watch.Start();
                HttpResponse response = HttpRequest(target);
                watch.Stop();
                string errorMessage = null;
                bool codeIsOk = false;
                if (response.TimedOut)
                {
                    errorMessage = "Http timedout : " + target.TimeoutMs + "ms";
                    watch.Reset();
                }
                else
                {
                    codeIsOk = response.HttpResponseCode == HttpStatusCode.OK;
                    if (!codeIsOk)
                    {
                        errorMessage = "response.HttpResponseCode: " + response.HttpResponseCode + Environment.NewLine + response.Content;
                        _log.Error(errorMessage);
                    }
                }
                bool success = codeIsOk && !response.TimedOut;
                ret = new GrabResponse(response.Content, success, watch.Elapsed, errorMessage);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ret = new GrabResponse(null, false, TimeSpan.Zero, "Error: " + ex.Message);
            }
            return ret;
        }

        private HttpResponse HttpRequest(IGrabTarget target)
        {
            var httpTarget = target as HttpTarget;
            _log.Debug($"Grabbing: {httpTarget.Method} {httpTarget.Uri}");
            SetSwitch(SWITCH_HTTP2_SUPPORT, true);
            SetSwitch(SWITCH_USE_SOCKET_HTTP_HANDLER, true);
#if HTTP_DEBUG
            DisplaySwitch(SWITCH_HTTP2_SUPPORT);
            DisplaySwitch(SWITCH_USE_SOCKET_HTTP_HANDLER);
#endif
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12;
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(httpTarget.Uri),
                Method = httpTarget.Method,
            };
            request.Headers.Clear();
            if (httpTarget.Headers.Count != 0)
            {
                SetHeaders(httpTarget, request);
            }
            else
            {
                DefaultHeaders(request);
            }
            request.Version = new Version(2, 0);
            //            var client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            var handler = new HttpClientHandler();
            //X509Certificate2 certificate = GetMyX509Certificate();
            //handler.ClientCertificates.Add(certificate);
            handler.UseDefaultCredentials = false;
            handler.SslProtocols = SslProtocols.Tls13 | SslProtocols.Tls12;// | SslProtocols.Tls | SslProtocols.Ssl3 | SslProtocols.Ssl2;
            handler.ServerCertificateCustomValidationCallback = CertificateValidationCallback;
            var client = new HttpClient(handler);
            if (target.TimeoutMs > 0)
            {
                client.Timeout = TimeSpan.FromMilliseconds(target.TimeoutMs);
            }
            var ret = new HttpResponse();
            try
            {
                _log.Debug("Send request...");
                var requestTask = client.SendAsync(request);
                _log.Debug("request is sent");
                var response = requestTask.Result;
                _log.Debug("result is loaded");
                ret.HttpResponseCode = response.StatusCode;
                _log.Debug("HttpResponseCode =" + ret.HttpResponseCode);
                ret.Content = response.Content.ReadAsStringAsync().Result;
                ret.TimedOut = false;
                _log.Debug("response is loaded");
            }
            catch (Exception e)
            {
                if (e.InnerException?.InnerException is AuthenticationException)
                {
                    _log.Error("SSL link cannot established: Maybe there is incompatibility between ciphers of Server and local Operating system");
                    _log.Error(e);
                    throw new Exception("SSL link cannot established");
                }
                else if (e.InnerException is TaskCanceledException)
                {
                    _log.Error("Timeout trigerred!");
                    ret.TimedOut = true;
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                handler.Dispose();
                client.Dispose();
            }
            return ret;
        }

        private static void SetHeaders(HttpTarget httpTarget, HttpRequestMessage request)
        {
            foreach (var x in httpTarget.Headers)
            {
                request.Headers.Add(x.Key, x.Value);
            }
        }

        private static void DefaultHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("User-Agent", "Mozilla/5.0");
            //request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("Accept-Language", "fr,fr-FR;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Cache-Control", "max-age=0 ");
        }

        private bool CertificateValidationCallback(HttpRequestMessage message,  X509Certificate2 certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private void SetSwitch(string switchName, bool switchValue)
        {
            AppContext.SetSwitch(switchName, switchValue);
        }

        private void DisplaySwitch(string switchName)
        {
            bool switchValue;
            if (AppContext.TryGetSwitch(switchName, out switchValue))
            {
                _log.Debug(switchName + ": " + switchValue);
            }
            else
            {
                _log.Debug(switchName + ": not set");
            }
        }
    }
}