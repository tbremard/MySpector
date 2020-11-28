namespace MySpector.UnitTest
{
    public class TestSample
    {
        public IRump Rump { get; }
        public ITroxRule Rule { get; }

        public TestSample(string rumpContent, string rule)
        {
            Rump = CreateLocalRump(rumpContent);
            Rule = CreateLocaleRule(rule);

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
