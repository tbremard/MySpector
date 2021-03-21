namespace MySpector.Objects
{
    public class FileTarget : IGrabTarget
    {
        public string Path { get; private set; }
        public FileTarget(string path)
        {
            Path = path;
        }

        public GrabTargetType TargetType => GrabTargetType.FILE;
    }
}