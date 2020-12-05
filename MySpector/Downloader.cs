using System.Net.Http;

namespace MySpector
{

    public class Downloader
    {
        public Downloader()
        {
        }

        public static Downloader Create()
        {
            return new Downloader();
        }

        public HttpResponse HttpRequest(HttpTarget target)
        {
            var client = new HttpClient();
            var myGetTask = client.GetAsync(target.Uri);
            var response = myGetTask.Result;
            var ret = new HttpResponse();
            ret.HttpResponseCode = response.StatusCode;
            ret.Content = response.Content.ReadAsStringAsync().Result;
            return ret;
        }
    }
}