namespace MySpector.Objects
{
    public class XtraxDefinition
    {
        public int Order { get; }
        public XtraxType XtraxType { get; }
        public string Arg { get; }

        public XtraxDefinition(int order, XtraxType xtraxType, string arg)
        {
            Order = order;
            XtraxType = xtraxType;
            Arg = arg;
        }
    }
}