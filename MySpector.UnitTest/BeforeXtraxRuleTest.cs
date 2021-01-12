using MySpector.Models;
using NUnit.Framework;

namespace MySpector.UnitTest
{
    class BeforeXtraxRuleTest
    {
        BeforeXtrax _sut;
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("50.25", "    50.25   EUR", "EUR")]
        [TestCase("1,227.00", "1,227.00USD", "USD")]
        [TestCase(XtraxConst.NOT_FOUND, "  xxxxxxxxx   ", "EUR")]
        public void GetOutputChained_WhenContentIsThere_ThenFound(string expectedOutput, string content, string token)
        {
            _sut = new BeforeXtrax(token);
            var rump = DataTruck.CreateText(content);

            var data = _sut.GetOutputChained(rump);

            Assert.AreEqual(expectedOutput, data.GetText());
        }
    }
}
