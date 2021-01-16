namespace MySpector.Core
{
    public class CheckerParam
    {
        public CheckerType CheckerType { get; }
        public string Arg { get; }

        public CheckerParam(CheckerType checkerType, string arg)
        {
            this.CheckerType = checkerType;
            this.Arg = arg;
        }
    }
}