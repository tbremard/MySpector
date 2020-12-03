namespace MySpector
{
    public class BeforeTroxRule : XtraxRule
    {
        private readonly string _suffix;

        public BeforeTroxRule(string suffix)
        {
            _suffix = suffix;
        }

        protected override IInputData GetOutput(IInputData data)
        {
            IInputData ret;
            string content = data.GetText();
            if (!content.Contains(_suffix))
            {
                ret = InputData.CreateText(XtraxRuleConst.NOT_FOUND);
            }
            else
            {
                int index = content.IndexOf(_suffix);
                string contentExtracted = content.Substring(0, index);
                ret = InputData.CreateText(contentExtracted.Trim());
            }
            return ret;
        }
    }

}