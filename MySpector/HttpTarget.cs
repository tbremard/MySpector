namespace MySpector
{
    public class HttpTarget
    {
        public HttpTarget(string uri)
        {
            Uri = uri;
        }

        public string Uri;

        public static HttpTarget Create(string uri)
        {
            return new HttpTarget(uri);
        }
    }
}
