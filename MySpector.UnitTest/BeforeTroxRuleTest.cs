using NUnit.Framework;

namespace MySpector.UnitTest
{
    class BeforeTroxRuleTest
    {
        BeforeTroxRule _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new BeforeTroxRule("EUR");
        }

        [TestCase("50.25", "    50.25   EUR")]
        [TestCase(TroxRuleConst.NOT_FOUND, "  xxxxxxxxx   ")]
        public void ExtractData_WhenContentIsThere_ThenFound(string expectedOutput, string content)
        {
            var rump = new Rump(content);

            var data = _sut.GetOutput(rump);

            Assert.AreEqual(expectedOutput, data);
        }
    }

}
