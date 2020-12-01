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

        protected override string GetOutput(IRump rump)
        {
            string ret;
            string after = _after.GetOutputChained(rump);
            if (after == XtraxRuleConst.NOT_FOUND)
            {
                return XtraxRuleConst.NOT_FOUND;
            }
            var tempRump = new Rump(after);
            ret = _before.GetOutputChained(tempRump);
            return ret;
        }
    }
}