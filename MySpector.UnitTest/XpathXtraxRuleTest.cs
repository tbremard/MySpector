using MySpector.Objects;
using NUnit.Framework;
using System.Text.Json;
using MySpector.Objects.Args;

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
//            string jsonArg = "{\"Path\":\"" + Xpath + "\"}";
            var arg = new XpathArg() { Path = Xpath };
            var rump = DataTruck.CreateText(Html);
            var rootRule = new XpathXtrax(arg);
            var nextRule = new BetweenXtrax(new BetweenArg() { Prefix = "is:", Suffix = "EUR" });
            rootRule.SetNext(nextRule);

            var data = rootRule.GetOutputChained(rump);

            string ExpectedOutput = "1189,99";
            string actual = data.GetText();
            Assert.AreEqual(ExpectedOutput, actual);
        }

        [Test]
        public void Deserialize_WhenValid_ThenOk()
        {
            string jsonArg = "{\"Path\":\"xxx\"}";

            var xpathArg = JsonSerializer.Deserialize<XpathArg>(jsonArg);
            
            var expected = new XpathArg() { Path = "xxx" };
            Assert.AreEqual(expected.Path, xpathArg.Path);
        }

        [Test]
        public void FromJson_WhenValid_ThenOk()
        {
            string jsonArg = "{\"Path\":\"xxx\"}";

            var xpathArg = Jsoner.FromJson<XpathArg>(jsonArg);

            var expected = new XpathArg() { Path = "xxx" };
            Assert.AreEqual(expected.Path, xpathArg.Path);
        }

        [Test]
        public void ToJson_WhenValid_ThenOk()
        {
            var arg = new XpathArg() { Path = "xxx" };

            var json = Jsoner.ToJson(arg);
            
            string expected = "{\"Path\":\"xxx\"}";
            Assert.AreEqual(expected, json);
        }
    }
}
