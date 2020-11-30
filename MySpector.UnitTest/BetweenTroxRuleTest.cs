using NUnit.Framework;

namespace MySpector.UnitTest
{
    class BetweenTroxRuleTest
    {
        BetweenTroxRule _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new BetweenTroxRule("The Price of the item is:", "EUR");
        }

        [TestCase("50.25", "The Price of the item is: 50.25 EUR")]
        [TestCase(TroxRuleConst.NOT_FOUND, "  xxxxxxxxx   ")]
        public void ExtractData_WhenContentIsThere_ThenFound(string expectedOutput, string content)
        {
            var rump = new Rump(content);

            var data = _sut.GetOutput(rump);

            Assert.AreEqual(expectedOutput, data);
        }
    }
}
