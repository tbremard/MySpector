using System.Data;

namespace MySpector
{
    public class Data
    {
        public string Value { get; }
        public Data(string value)
        {
            Value = value;
        }
    }

    public class Trox
    {
        public Data ExtractData(Rump rump, Rule rule)
        {
            string value = "100";
            var ret = new Data(value);
            return ret;
        }
    }
}
