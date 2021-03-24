using System;

namespace MySpector.Objects
{
    public class GrabResponse
    {
        public string Content { get; }
        public bool Success { get; }
        public TimeSpan Latency { get; }
        public string ErrorMessage { get; }

        public GrabResponse(string content, bool success, TimeSpan latency, string errorMessage)
        {
            Content = content;
            Success = success;
            Latency = latency;
            ErrorMessage = errorMessage;
        }
    }

    public class InvalidResponse : GrabResponse
    {
        public InvalidResponse(string content, TimeSpan latency, string errorMessage)
            :base(content, false, latency, errorMessage)
        {

        }
    }
}