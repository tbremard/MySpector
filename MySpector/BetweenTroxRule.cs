namespace MySpector
{

    public class BetweenTroxRule : ITroxRule
    {
        private readonly ITroxRule _before;
        private readonly ITroxRule _after;

        public BetweenTroxRule(string prefix, string suffix)
        {
            _after = new AfterTroxRule(prefix);
            _before = new BeforeTroxRule(suffix);
        }

        public string GetOutput(IRump rump)
        {
            string ret;
            string after = _after.GetOutput(rump);
            if (after == TroxRuleConst.NOT_FOUND)
            {
                return TroxRuleConst.NOT_FOUND;
            }
            var tempRump = new Rump(after);
            ret = _before.GetOutput(tempRump);
            return ret;
        }
    }
}