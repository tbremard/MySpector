using System;

namespace MySpector.Objects
{
    public class GrabResponse
    {
        public string Content { get; }
        public bool Success { get; }
        public TimeSpan Latency { get; }

        public GrabResponse(string content, bool success, TimeSpan latency)
        {
            Content = content;
            Success = success;
            Latency = latency;
        }
    }

    public class InvalidResponse : GrabResponse
    {
        public InvalidResponse(string content, TimeSpan latency)
            :base(content, false, latency)
        {

        }
    }
}