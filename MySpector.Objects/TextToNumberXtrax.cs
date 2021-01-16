using System.Globalization;
using NLog;

namespace MySpector.Objects
{
    public class TextToNumberXtrax : Xtrax
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        protected override IDataTruck GetOutput(IDataTruck dataIn)
        {
            IDataTruck dataOut;
            string textNumber = dataIn.GetText();
            decimal ret;
            if (string.IsNullOrEmpty(textNumber))
            {
                _log.Error("Input: NULL");
                dataOut = new DataTruck(textNumber, null);
                return dataOut;
            }
            _log.Trace($"Input : '{dataIn.PreviewText}'");
            textNumber = textNumber.Replace(" ", null);
            textNumber = textNumber.Replace(",-", null);
            textNumber = textNumber.Replace(",–", null);
            textNumber = textNumber.Replace(".-", null);
            textNumber = textNumber.Replace(".–", null);
            var provider = new NumberFormatInfo();
            if (textNumber.Contains('.') && textNumber.Contains(','))
            {
                int indexOfPoint = textNumber.IndexOf('.');
                int indexOfComa = textNumber.IndexOf(',');
                if (indexOfPoint > indexOfComa)
                {
                    provider.NumberDecimalSeparator = ".";
                    provider.NumberGroupSeparator = ",";
                    provider.CurrencyDecimalSeparator = ".";
                    provider.CurrencyGroupSeparator = ",";
                }
                else
                {
                    provider.NumberDecimalSeparator = ",";
                    provider.NumberGroupSeparator = ".";
                    provider.CurrencyDecimalSeparator = ",";
                    provider.CurrencyGroupSeparator = ".";
                }
                if (decimal.TryParse(textNumber, NumberStyles.Any, provider, out ret))
                {
                    _log.Trace($"Output: [{ret}]");
                    dataOut = new DataTruck(textNumber, ret);
                    return dataOut;
                }
            }
            else
            {
                if (textNumber.Contains(','))
                {
                    if (ThreeDigitsAfterComa(textNumber))
                    {
                        textNumber = textNumber.Replace(",", string.Empty);
                    }
                    provider.NumberGroupSeparator = "";
                    provider.NumberDecimalSeparator = ",";
                    if (decimal.TryParse(textNumber, NumberStyles.Any, provider, out ret))
                    {
                        _log.Trace($"Output: [{ret}]");
                        dataOut = new DataTruck(textNumber, ret);
                        return dataOut;
                    }
                }
                if (textNumber.Contains('.'))
                {
                    provider.NumberGroupSeparator = "";
                    provider.NumberDecimalSeparator = ".";
                    if (decimal.TryParse(textNumber, NumberStyles.Any, provider, out ret))
                    {
                        _log.Trace($"Output: [{ret}]");
                        dataOut = new DataTruck(textNumber, ret);
                        return dataOut;
                    }
                }
            }
            if (decimal.TryParse(textNumber, out ret))
            {
                _log.Trace($"Output: [{ret}]");
                dataOut = new DataTruck(textNumber, ret);
                return dataOut;
            }
            dataOut = new DataTruck(textNumber, null);
            _log.Error($"Output: Cannot transform to number");
            return dataOut;
        }

        private bool ThreeDigitsAfterComa(string textNumber)
        {
            int indexOfComa = textNumber.IndexOf(',');
            if (indexOfComa == textNumber.Length)
                return false;
            string afterComa = textNumber.Substring(indexOfComa + 1);
            bool ret = afterComa.Length == 3 ? true : false;
            return ret;
        }
    }
}
