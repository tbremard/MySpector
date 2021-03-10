namespace MySpector.Objects
{
    public class XtraxDefinition
    {
        public int Order { get; }
        public XtraxType XtraxType { get; }
        public string Arg { get; }
        public int? DbId { get; }

        public XtraxDefinition(int order, XtraxType xtraxType, string arg, int? dbId)
        {
            Order = order;
            XtraxType = xtraxType;
            Arg = arg;
            DbId = dbId;
        }
    }
}