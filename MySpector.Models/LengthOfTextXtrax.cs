﻿using NLog;

namespace MySpector.Models
{
    public class LengthOfTextXtrax : Xtrax
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            string content = data.GetText();
            if (string.IsNullOrEmpty(content))
            {
                _log.Error("Text is empty : return 0");
                return DataTruck.CreateNumber(0);
            }
            string trimmed = content.Trim();
            ret = DataTruck.CreateNumber(trimmed.Length);
            _log.Trace("Length of [" + data.PreviewText + "] :" + trimmed.Length);
            return ret;
        }
    }
}