namespace MySpector.Objects
{
    public class CheckerParam
    {
        public CheckerType CheckerType { get; }
        public string Arg { get; }

        public CheckerParam(CheckerType checkerType, string arg)
        {
            CheckerType = checkerType;
            Arg = arg;
        }
    }
}