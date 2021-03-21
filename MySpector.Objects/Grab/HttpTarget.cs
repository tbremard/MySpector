using System.Collections.Generic;
using System.Net.Http;

namespace MySpector.Objects
{
    public class HttpTarget : IGrabTarget
    {
        public int? DbId { get; set; }
        public HttpMethod Method { get; set; }
        public string Uri { get; set; }
        public string Version { get; set; }
        public IList<HeaderEntry> Headers { get; }
        public string Content { get; set; }
        public TargetType TargetType => TargetType.HTTP;

        public HttpTarget(string uri)
        {
            Uri = uri;
            Headers = new List<HeaderEntry>();
            Version = "2.0";
            Method = HttpMethod.Get;
        }

        public static HttpTarget Create(string uri)
        {
            return new HttpTarget(uri);
        }

        public override string ToString()
        {
            return "HttpTarget->"+ Uri;
        }
    }

    public class InvalidTarget : IGrabTarget
    {
        public TargetType TargetType => throw new System.NotImplementedException();
    }
}

