using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class XpathXtraxRuleTest
    {
        [Test]
        public void GetOutputChained_When2RulesAreChained_ThenDataIsExtracted()
        {
            string Html = "<html><head></head><body><div class=\"Z2\"><strong class=\"Z9v\">The price of the item is: 1189,99 EUR</strong></div></body></html>";
            string Xpath = "/html/body/div/strong";
            var rump = DataTruck.CreateText(Html);
            var rootRule = new XpathXtraxRule(Xpath);
            var nextRule = new BetweenXtraxRule("is:", "EUR");
            rootRule.SetNext(nextRule);

            var data = rootRule.GetOutputChained(rump);

            string ExpectedOutput = "1189,99";
            string actual = data.GetText();
            Assert.AreEqual(ExpectedOutput, actual);
        }
    }
}
