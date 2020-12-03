﻿namespace MySpector.UnitTest
{
    public class TestSample
    {
        public IDataTruck Data { get; }
        public XtraxRule Rule { get; }
        public string ExpectedOutput { get; }

        public TestSample(string rumpContent, string rule, string expectedOutput)
        {
            Data = CreateLocalData(rumpContent);
            Rule = CreateLocaleRule(rule);
            ExpectedOutput = expectedOutput;
        }

        private XtraxRule CreateLocaleRule(string rule)
        {
            var ret = new XpathXtraxRule(rule);
            return ret;
        }

        private IDataTruck CreateLocalData(string content)
        {
            var ret = DataTruck.CreateText(content);
            return ret;
        }
    }
}
