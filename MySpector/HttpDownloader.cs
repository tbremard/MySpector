using System;
using System.Net;
using System.Net.Http;
using NLog;
using System.Security.Authentication;
using MySpector.Core;

namespace MySpector
{
    public class HttpDownloader
    {
        const string SWITCH_HTTP2_SUPPORT = "System.Net.Http.SocketsHttpHandler.Http2Support";
        const string SWITCH_USE_SOCKET_HTTP_HANDLER = "System.Net.Http.UseSocketsHttpHandler";

        static Logger _log = LogManager.GetCurrentClassLogger();

        public HttpDownloader()
        {
        }

        public static HttpDownloader Create()
        {
            return new HttpDownloader();
        }

        public HttpResponse HttpRequest(HttpTarget target)
        {
            SetSwitch(SWITCH_HTTP2_SUPPORT, true);
            SetSwitch(SWITCH_USE_SOCKET_HTTP_HANDLER, true);
#if HTTP_DEBUG
            DisplaySwitch(SWITCH_HTTP2_SUPPORT);
            DisplaySwitch(SWITCH_USE_SOCKET_HTTP_HANDLER);
#endif
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12;
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(target.Uri),
                Method = HttpMethod.Get,
            };
            request.Headers.Clear();
//            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:83.0) Gecko/20100101 Firefox/83.0");
            request.Headers.Add("User-Agent", "Mozilla/5.0");
            //request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("Accept-Language", "fr,fr-FR;q=0.8,en-US;q=0.5,en;q=0.3");
//            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Cache-Control", "max-age=0 ");
            request.Version = new Version(2,0);
            //            var client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            var handler = new HttpClientHandler();
            //X509Certificate2 certificate = GetMyX509Certificate();
            //handler.ClientCertificates.Add(certificate);
            handler.UseDefaultCredentials = true;
            handler.SslProtocols = SslProtocols.Tls13| SslProtocols.Tls12;
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var client = new HttpClient(handler);
            var myGetTask = client.SendAsync(request);
            var response = myGetTask.Result;
            var ret = new HttpResponse();
            ret.HttpResponseCode = response.StatusCode;
            ret.Content = response.Content.ReadAsStringAsync().Result;
            handler.Dispose();
            client.Dispose();
            return ret;
        }

        public DownloadResponse Download(WatchItem item)
        {
            DownloadResponse ret;
            _log.Debug(item.Url);
            try
            {
                HttpTarget target = HttpTarget.Create(item.Url);
                HttpResponse response = HttpRequest(target);
                bool success = response.HttpResponseCode == HttpStatusCode.OK;
                ret = new DownloadResponse(response.Content, success);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ret = new DownloadResponse(string.Empty, false);
            }
            return ret;
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