using NLog;
using System;

namespace MySpector
{
    public class TextReplaceXtraxRule : XtraxRule
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        string _oldToken; 
        string _newToken;

        public TextReplaceXtraxRule(string oldToken, string newToken)
        {
            _oldToken = oldToken;
            _newToken = newToken;
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            string text = data.GetText();
            IDataTruck ret;
            if (string.IsNullOrEmpty(text))
            {
                return DataTruck.CreateText(string.Empty);
            }
            if (string.IsNullOrEmpty(_oldToken))
            {
                return DataTruck.CreateText(text);
            }
            try
            {
                string replaced = text.Replace(_oldToken, _newToken);
                ret = DataTruck.CreateText(replaced);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ret = DataTruck.CreateText(XtraxRuleConst.NOT_FOUND);
            }
            return ret;
        }
    }
}
