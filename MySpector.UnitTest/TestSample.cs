namespace MySpector.UnitTest
{
    public class TestSample
    {
        public IDataTruck Data { get; }
        public XtraxRule Rule { get; }
        public string ExpectedOutput { get; }
        public string Name { get; }

        public TestSample(string name, string rumpContent, string rule, string expectedOutput)
        {
            Data = CreateLocalData(rumpContent);
            Rule = CreateXpathRule(rule);
            ExpectedOutput = expectedOutput;
            Name = name;
        }

        private XtraxRule CreateXpathRule(string rule)
        {
            var ret = new XpathXtraxRule(rule);// do not chain other rules here because some test rely on it
            return ret;
        }

        private IDataTruck CreateLocalData(string content)
        {
            var ret = DataTruck.CreateText(content);
            return ret;
        }
    }
}
