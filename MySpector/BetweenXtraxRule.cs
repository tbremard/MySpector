namespace MySpector
{
    public class BetweenXtraxRule : XtraxRule
    {
        private readonly XtraxRule _before;
        private readonly XtraxRule _after;

        public BetweenXtraxRule(string prefix, string suffix)
        {
            _after = new AfterTroxRule(prefix);
            _before = new BeforeTroxRule(suffix);
        }

        protected override IInputData GetOutput(IInputData data)
        {
            IInputData ret;
            var after = _after.GetOutputChained(data);
            if (after.GetText() == XtraxRuleConst.NOT_FOUND)
            {
                return InputData.CreateText(XtraxRuleConst.NOT_FOUND);
            }
            ret = _before.GetOutputChained(after);
            return ret;
        }
    }
}