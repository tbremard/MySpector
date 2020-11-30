namespace MySpector
{
    public class BeforeTroxRule : ITroxRule
    {
        private readonly string _suffix;

        public BeforeTroxRule(string suffix)
        {
            _suffix = suffix;
        }

        public string GetOutput(IRump rump)
        {
            string ret;
            if (!rump.Content.Contains(_suffix))
            {
                ret = TroxRuleConst.NOT_FOUND;
            }
            else
            {
                int index = rump.Content.IndexOf(_suffix);
                string contentExtracted = rump.Content.Substring(0, index);
                ret = contentExtracted.Trim();
            }
            return ret;
        }
    }

}