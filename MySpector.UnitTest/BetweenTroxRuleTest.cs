using NUnit.Framework;

namespace MySpector.UnitTest
{
    class BetweenTroxRuleTest
    {
        BetweenXtraxRule _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new BetweenXtraxRule("The Price of the item is:", "EUR");
        }

        [TestCase("50.25", "The Price of the item is: 50.25 EUR")]
        [TestCase(XtraxRuleConst.NOT_FOUND, "  xxxxxxxxx   ")]
        public void GetOutputChained_WhenContentIsThere_ThenFound(string expectedOutput, string content)
        {
            var rump = DataTruck.CreateText(content);

            var data = _sut.GetOutputChained(rump);

            Assert.AreEqual(expectedOutput, data.GetText());
        }
    }
}
