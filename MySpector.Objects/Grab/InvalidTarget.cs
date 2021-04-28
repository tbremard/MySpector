namespace MySpector.Objects
{
    public class InvalidTarget : IGrabTarget
    {
        public string Name => "InvalidTarget";
        public int TimeoutMs => 0;
        public TargetType TargetType => throw new System.NotImplementedException();
    }
}

