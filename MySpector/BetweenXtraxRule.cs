using MySpector.Core;

namespace MySpector
{
    public class BetweenXtraxRule : XtraxRule
    {
        private readonly XtraxRule _before;
        private readonly XtraxRule _after;

        public BetweenXtraxRule(string prefix, string suffix)
        {
            _after = new AfterXtraxRule(prefix);
            _before = new BeforeXtraxRule(suffix);
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            var after = _after.GetOutputChained(data);
            if (after.GetText() == XtraxRuleConst.NOT_FOUND)
            {
                return DataTruck.CreateText(XtraxRuleConst.NOT_FOUND);
            }
            ret = _before.GetOutputChained(after);
            return ret;
        }
    }
}