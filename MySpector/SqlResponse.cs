namespace MySpector.Core
{
    public class SqlResponse
    {
        public bool IsOk { get; }
        public string Content { get; }
        public SqlResponse(bool isOk, string content)
        {
            IsOk = isOk;
            Content = content;
        }
    }
}