using MySpector.Models;

namespace MySpector.UnitTest
{
    public class TestSample
    {
        public IDataTruck Data { get; }
        public Xtrax Rule { get; }
        public string ExpectedOutput { get; }
        public string Name { get; }

        public TestSample(string name, string rumpContent, string rule, string expectedOutput)
        {
            Data = CreateLocalData(rumpContent);
            Rule = CreateXpathRule(rule);
            ExpectedOutput = expectedOutput;
            Name = name;
        }

        private Xtrax CreateXpathRule(string rule)
        {
            var ret = new XpathXtrax(rule);// do not chain other rules here because some test rely on it
            return ret;
        }

        private IDataTruck CreateLocalData(string content)
        {
            var ret = DataTruck.CreateText(content);
            return ret;
        }
    }
}
