using NUnit.Framework;

namespace MySpector.UnitTest
{
    class BeforeTroxRuleTest
    {
        BeforeTroxRule _sut;
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("50.25", "    50.25   EUR", "EUR")]
        [TestCase("1,227.00", "1,227.00USD", "USD")]
        [TestCase(TroxRuleConst.NOT_FOUND, "  xxxxxxxxx   ", "EUR")]
        public void ExtractData_WhenContentIsThere_ThenFound(string expectedOutput, string content, string token)
        {
            _sut = new BeforeTroxRule(token);
            var rump = new Rump(content);

            var data = _sut.GetOutput(rump);

            Assert.AreEqual(expectedOutput, data);
        }
    }
}
