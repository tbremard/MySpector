namespace MySpector
{
    public class AfterTroxRule : XtraxRule
    {
        private readonly string _prefix;

        public AfterTroxRule(string prefix)
        {
            _prefix = prefix;
        }

        protected override IInputData GetOutput(IInputData data)
        {
            IInputData ret;
            string content = data.GetText();
            if (!content.Contains(_prefix))
            {
                ret = InputData.CreateText(XtraxRuleConst.NOT_FOUND);
            }
            else
            {
                int index = content.IndexOf(_prefix);
                int indexAfter = index + _prefix.Length;
                string contentAfter = content.Substring(indexAfter);
                ret = InputData.CreateText(contentAfter.Trim());
            }
            return ret;
        }
    }

}