using System.Globalization;

namespace MySpector
{
    public class Trox
    {
        public Data ExtractData(IRump rump, XtraxRule rule)
        {
            string value = rule.GetOutputChained(rump);
            var ret = new Data(value);
            return ret;
        }

        public decimal TransformTextToNumber(string textNumber)
        {
            decimal ret;
            if (string.IsNullOrEmpty(textNumber))
                return 0;
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
                    return ret;
            }
            else
            {
                if (textNumber.Contains(','))
                {
                    provider.NumberGroupSeparator = "";
                    provider.NumberDecimalSeparator = ",";
                    if (decimal.TryParse(textNumber, NumberStyles.Any, provider, out ret))
                        return ret;
                }
                if (textNumber.Contains('.'))
                {
                    provider.NumberGroupSeparator = "";
                    provider.NumberDecimalSeparator = ".";
                    if (decimal.TryParse(textNumber, NumberStyles.Any, provider, out ret))
                        return ret;
                }
            }
            if (decimal.TryParse(textNumber, out ret))
            {
                return ret;
            }
            return 0;
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
