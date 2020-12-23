using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class XpathXtraxRuleTest
    {
        [SetUp]
        public void Setup()
        {
            TestSampleFactory.Setup();
        }

        [TestCase(TestSampleId.ZOTAC_EN72070V_GALAXUS)]
        [TestCase(TestSampleId.ZOTAC_EN72070V_GALAXUS_FULL_PAGE)]
        [TestCase(TestSampleId.PS4_SATURN)]
        [TestCase(TestSampleId.PS4_SATURN_FULL_PAGE)]
        [TestCase(TestSampleId.BDIY_FULL_PAGE)]
        [TestCase(TestSampleId.HYSTOU_F7_FULL_PAGE)]
        public void GetOutputChained_WhenXpathIsValidAndContentIsThere_ThenFound(TestSampleId sampleId)
        {
            var sample = TestSampleFactory.CreateSample(sampleId);

            var data = sample.Rule.GetOutputChained(sample.Data);

            string actual = data.GetText();
            Assert.AreEqual(sample.ExpectedOutput, actual);
        }

        [Test]
        public void GetOutputChained_When2RulesAreChained_ThenDataIsExtracted()
        {
            string Html = "<html><head></head><body><div class=\"Z2\"><strong class=\"Z9v\">The price of the item is: 1189,99 EUR</strong></div></body></html>";
            string Xpath = "/html/body/div/strong";
            var rump = DataTruck.CreateText(Html);
            var rootRule = new XpathXtrax(Xpath);
            var nextRule = new BetweenXtrax("is:", "EUR");
            rootRule.SetNext(nextRule);

            var data = rootRule.GetOutputChained(rump);

            string ExpectedOutput = "1189,99";
            string actual = data.GetText();
            Assert.AreEqual(ExpectedOutput, actual);
        }
    }
}
