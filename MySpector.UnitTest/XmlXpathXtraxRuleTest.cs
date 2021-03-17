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
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><a><b>xaxa</b><b>toto</b></a>";
            string Xpath = "/a/b[2]";
            var arg = new XpathArg() { Path = Xpath };
            var rump = DataTruck.CreateText(xml);
            var rootRule = new XmlXpathXtrax(arg);

            var data = rootRule.GetOutputChained(rump);

            string ExpectedOutput = "toto";
            string actual = data.GetText();
            Assert.AreEqual(ExpectedOutput, actual);
        }
    }
}
