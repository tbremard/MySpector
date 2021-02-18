using MySpector.Objects;
using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class AfterXtraxRuleTest
    {
        AfterXtrax _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new AfterXtrax(new AfterArg() { Prefix = "The Price of the item is:" });
        }

        [TestCase("50.25", "The Price of the item is:    50.25   ")]
        [TestCase(XtraxConst.NOT_FOUND, "  xxxxxxxxx   ")]
        public void GetOutputChained_WhenContentIsThere_ThenFound(string expectedOutput, string content)
        {
            var rump = DataTruck.CreateText(content);

            var data = _sut.GetOutputChained(rump);

            Assert.AreEqual(expectedOutput, data.GetText());
        }
    }
}
