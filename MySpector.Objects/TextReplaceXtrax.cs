using NLog;
using System;

namespace MySpector.Objects
{
    public class TextReplaceXtrax : Xtrax
    {
        public override XtraxType Type => XtraxType.TextReplace;
        static Logger _log = LogManager.GetCurrentClassLogger();
        string _oldToken;
        string _newToken;

        public TextReplaceXtrax(string oldToken, string newToken)
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
                _log.Error("Input is empty: cannot replace content");
                return DataTruck.CreateText(string.Empty);
            }
            _log.Trace("Replacing content of '" + data.PreviewText + "'");
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
                ret = DataTruck.CreateText(XtraxConst.NOT_FOUND);
            }
            return ret;
        }
    }
}
