namespace MySpector.Objects
{
    public class FileTarget : IWebTarget
    {
        public string Path { get; private set; }
        public FileTarget(string path)
        {
            Path = path;
        }

        public WebTargetType WebTargetType => WebTargetType.FILE;
    }
}