namespace MySpector
{
    public class AfterTroxRule : ITroxRule
    {
        private readonly string _prefix;

        public AfterTroxRule(string prefix)
        {
            _prefix = prefix;
        }

        public string GetOutput(IRump rump)
        {
            string ret;
            if (!rump.Content.Contains(_prefix))
            {
                ret = TroxRuleConst.NOT_FOUND;
            }
            else
            {
                int index = rump.Content.IndexOf(_prefix);
                int indexAfter = index + _prefix.Length;
                string contentAfter = rump.Content.Substring(indexAfter);
                ret = contentAfter.Trim();
            }
            return ret;
        }
    }

}