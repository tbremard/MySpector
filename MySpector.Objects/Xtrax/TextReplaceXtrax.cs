using MySpector.Objects.Args;
using NLog;
using System;

namespace MySpector.Objects
{
    public class TextReplaceXtrax : Xtrax
    {
        public override XtraxType Type => XtraxType.TextReplace;
        public override string JsonArg { get { return Jsoner.ToJson(_arg); } }

        static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly TextReplaceArg _arg;

        public TextReplaceXtrax(TextReplaceArg arg)
        {
            this._arg = arg;
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            string text = data.GetText();
            IDataTruck ret;
            if (string.IsNullOrEmpty(text))
            {
                string message = "Input is empty: cannot replace content";
                _log.Error(message);
                ErrorMessage.AppendLine(message);
                return DataTruck.CreateText(string.Empty);
            }
            _log.Trace("Replacing content of '" + data.PreviewText + "'");
            if (string.IsNullOrEmpty(_arg.OldToken))
            {
                return DataTruck.CreateText(text);
            }
            try
            {
                string replaced = text.Replace(_arg.OldToken, _arg.NewToken);
                ret = DataTruck.CreateText(replaced);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ErrorMessage.AppendLine(ex.Message);
                ret = DataTruck.CreateText(XtraxConst.NOT_FOUND);
            }
            return ret;
        }
    }
}
