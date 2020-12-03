using System.Globalization;

namespace MySpector
{
    public class Trox
    {
        public IDataTruck TransformTextToNumber(IDataTruck dataIn)
        {
            IDataTruck dataOut;
            string textNumber = dataIn.GetText();
            decimal ret;
            if (string.IsNullOrEmpty(textNumber))
            {
                dataOut = new DataTruck(textNumber, null);
                return dataOut;
            }
            textNumber = textNumber.Replace(" ", null);
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
                    dataOut = new DataTruck(textNumber, ret);
                    return dataOut;
                }
            }
            else
            {
                if (textNumber.Contains(','))
                {
                    provider.NumberGroupSeparator = "";
                    provider.NumberDecimalSeparator = ",";
                    if (decimal.TryParse(textNumber, NumberStyles.Any, provider, out ret))
                    {
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
                        dataOut = new DataTruck(textNumber, ret);
                        return dataOut;
                    }
                }
            }
            if (decimal.TryParse(textNumber, out ret))
            {
                dataOut = new DataTruck(textNumber, ret);
                return dataOut;
            }
            dataOut = new DataTruck(textNumber, null);
            return dataOut;
        }

        public string TransformTextReplace(string text, string oldToken, string newToken)
        {
            string ret;
            if (string.IsNullOrEmpty(text))
            { 
                return string.Empty; 
            }
            if (string.IsNullOrEmpty(oldToken))
            {
                return text;
            }
            ret = text.Replace(oldToken, newToken);
            return ret;
        }
    }
}
