using System.Net;

namespace MySpector
{
    public class HttpResponse
    {
        public HttpStatusCode HttpResponseCode;
        public string Content;
        public bool TimedOut;

        public HttpResponse()
        {
            Content = string.Empty;
            TimedOut = false;
        }
    }
}