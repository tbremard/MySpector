﻿using System.Collections.Generic;
using System.Net.Http;
namespace MySpector
{
    public class HttpTarget
    {
        public HttpMethod Method { get; set; }
        public string RequestUri { get; set; }
        public string Version { get; set; }
        public List<KeyValuePair<string,string>> Headers { get; }
        public string Content { get; set; }

        public HttpTarget(string uri)
        {
            RequestUri = uri;
            Headers = new List<KeyValuePair<string, string>>();
            Version = "2.0";
            Method = HttpMethod.Get;
        }

        public static HttpTarget Create(string uri)
        {
            return new HttpTarget(uri);
        }
    }
}
