using MySpector.Objects;

namespace MySpector
{
    public class BetweenXtrax : Xtrax
    {
        public override XtraxType Type => XtraxType.Between;
        public override string JsonArg { get { return Jsoner.ToJson(_arg); } }

        private readonly Xtrax _before;
        private readonly Xtrax _after;
        private readonly BetweenArg _arg;

        public BetweenXtrax(BetweenArg arg)
        {
            this._arg = arg;
            _before = new BeforeXtrax(new BeforeArg() { Suffix = arg.Suffix });
            _after = new AfterXtrax(new AfterArg() { Prefix= arg.Prefix});
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            var after = _after.GetOutputChained(data);
            if (after.GetText() == XtraxConst.NOT_FOUND)
            {
                return DataTruck.CreateText(XtraxConst.NOT_FOUND);
            }
            ret = _before.GetOutputChained(after);
            return ret;
        }
    }
}