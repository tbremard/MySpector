namespace MySpector.UnitTest
{
    public class TestSample
    {
        public IRump Rump { get; }
        public ITroxRule Rule { get; }
        public string ExpectedOutput { get; }

        public TestSample(string rumpContent, string rule, string expectedOutput)
        {
            Rump = CreateLocalRump(rumpContent);
            Rule = CreateLocaleRule(rule);
            ExpectedOutput = expectedOutput;
        }

        private ITroxRule CreateLocaleRule(string rule)
        {
            var ret = new XpathTroxRule(rule);
            return ret;
        }

        private Rump CreateLocalRump(string content)
        {
            var ret = new Rump(content);
            return ret;
        }
    }
}
