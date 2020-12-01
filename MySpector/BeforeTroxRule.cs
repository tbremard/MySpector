namespace MySpector
{
    public class BeforeTroxRule : XtraxRule
    {
        private readonly string _suffix;

        public BeforeTroxRule(string suffix)
        {
            _suffix = suffix;
        }

        protected override string GetOutput(IRump rump)
        {
            string ret;
            if (!rump.Content.Contains(_suffix))
            {
                ret = XtraxRuleConst.NOT_FOUND;
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