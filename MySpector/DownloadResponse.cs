namespace MySpector.Core
{
    public class DownloadResponse
    {
        public string Content { get; set; }
        public bool Success { get; set; }
        public DownloadResponse(string content, bool success)
        {
            Content = content;
            Success = success;

        }
    }
}