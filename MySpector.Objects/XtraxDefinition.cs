using MySpector.Objects;

namespace MySpector
{
    public class XtraxDefinition
    {
        public int Order { get; }
        public XtraxType XtraxType { get;}
        public string Arg { get;}

        public XtraxDefinition(int order, XtraxType xtraxType, string arg)
        {
            this.Order = order;
            this.XtraxType = xtraxType;
            this.Arg = arg;
        }
    }
}