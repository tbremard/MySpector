namespace MySpector.Objects
{
    public class FileTarget : IGrabTarget
    {
        public TargetType TargetType => TargetType.FILE;
        public string Name { get; set; }
        public string Path { get; private set; }

        public FileTarget(string path)
        {
            Path = path;
        }

    }

    public class ProcessTarget : IGrabTarget
    {
        public TargetType TargetType => TargetType.PROCESS;
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Arguments { get; set; }
        public int? WaitForExitMs { get; set; }
        public string StandardInput { get; set; }
    }
}