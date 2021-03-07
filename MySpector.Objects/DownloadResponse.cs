using System;

namespace MySpector.Objects
{
    public class DownloadResponse
    {
        public string Content { get; }
        public bool Success { get; }
        public TimeSpan Latency { get; }

        public DownloadResponse(string content, bool success, TimeSpan latency)
        {
            Content = content;
            Success = success;
            Latency = latency;
        }
    }

    public class InvalidResponse : DownloadResponse
    {
        public InvalidResponse(string content, TimeSpan latency)
            :base(content, false, latency)
        {

        }
    }
}