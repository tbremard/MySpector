using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using NLog;

namespace MySpector
{
    public class Downloader
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public Downloader()
        {
        }

        public static Downloader Create()
        {
            return new Downloader();
        }

        public HttpResponse HttpRequest(HttpTarget target)
        {
            return req2(target);
            var client = new HttpClient();
            var myGetTask = client.GetAsync(target.Uri);
            var response = myGetTask.Result;
            var ret = new HttpResponse();
            ret.HttpResponseCode = response.StatusCode;
            ret.Content = response.Content.ReadAsStringAsync().Result;
            return ret;
        }

        public HttpResponse req2(HttpTarget target)
        {
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(target.Uri),
                Method = HttpMethod.Get,
            };
            //          request.Headers.Add("Host", "www.saturn.de");
            request.Headers.Clear();
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:83.0) Gecko/20100101 Firefox/83.0");
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            request.Headers.Add("Accept-Language", "fr,fr-FR;q=0.8,en-US;q=0.5,en;q=0.3");
//            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Cache-Control", "max-age=0 ");
            request.Version = new Version(1,1);
            //            var client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            var handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            var client = new HttpClient(handler);
            var myGetTask = client.SendAsync(request);
            var response = myGetTask.Result;
            var ret = new HttpResponse();
            ret.HttpResponseCode = response.StatusCode;
            ret.Content = response.Content.ReadAsStringAsync().Result;
            return ret;
        }
    }

    public class LoggingHandler : DelegatingHandler
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _log.Debug("Request:");
            _log.Debug(request.ToString());
            if (request.Content != null)
            {
                _log.Debug(await request.Content.ReadAsStringAsync());
            }
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            _log.Debug("Response:");
            _log.Debug(response.ToString());
            if (response.Content != null)
            {
                _log.Debug(await response.Content.ReadAsStringAsync());
            }
            return response;
        }
    }
}