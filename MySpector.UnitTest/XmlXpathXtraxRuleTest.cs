using MySpector.Objects;
using NUnit.Framework;
using MySpector.Objects.Args;

namespace MySpector.UnitTest
{
    public class XmlXpathXtraxRuleTest
    {
        [SetUp]
        public void Setup()
        {
            TestSampleFactory.Setup();
        }

        [Test]
        public void GetOutputChained_WhenXmlIsValid_ThenDataIsExtracted()
        {
            string Html = "<html><head></head><body><div class=\"Z2\"><strong class=\"Z9v\">The price of the item is: 1189,99 EUR</strong></div></body></html>";
            string Xpath = "/html/body/div/strong";
            var arg = new XpathArg() { Path = Xpath };
            var rump = DataTruck.CreateText(Html);
            var rootRule = new XmlXpathXtrax(arg);

            var data = rootRule.GetOutputChained(rump);

            string ExpectedOutput = "1189,99";
            string actual = data.GetText();
            Assert.AreEqual(ExpectedOutput, actual);
        }
    }
}
