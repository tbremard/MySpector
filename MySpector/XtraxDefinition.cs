using MySpector.Core;

namespace MySpector
{
    public class XtraxDefinition
    {
        public XtraxType XtraxType { get; set; }
        public string Arg { get; set; }

        public XtraxDefinition(XtraxType xtraxType, string arg)
        {
            this.XtraxType = xtraxType;
            this.Arg = arg;
        }
    }
}