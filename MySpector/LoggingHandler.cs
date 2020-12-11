using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using NLog;

namespace MySpector
{
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