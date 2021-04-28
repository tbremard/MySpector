namespace MySpector.Objects
{
    public class ProcessTarget : IGrabTarget
    {
        public TargetType TargetType => TargetType.PROCESS;
        public string Name { get; set; }
        public int TimeoutMs { get; set; }
        public string FileName { get; set; }
        public string Arguments { get; set; }
        public string StandardInput { get; set; }
    }
}