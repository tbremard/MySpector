﻿using System.Net.Http;
using System.Net;

namespace MySpector
{
    public class HttpResponse
    {
        public HttpStatusCode HttpResponseCode;
        public string Content;
    }

    public class FileScraper
    {
        public FileScraper()
        {
        }

        public HttpResponse HttpDownload(HttpTarget httpTarget)
        {
            var client = new HttpClient();
            var myGetTask = client.GetAsync(httpTarget.Uri);
            var response = myGetTask.Result;
            var ret = new HttpResponse();
            ret.HttpResponseCode = response.StatusCode;
            ret.Content = response.Content.ReadAsStringAsync().Result;
            return ret;
        }
    }
}