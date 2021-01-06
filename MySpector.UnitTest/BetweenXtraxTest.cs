using NUnit.Framework;

namespace MySpector.UnitTest
{
    class BetweenXtraxTest
    {
        BetweenXtrax _sut;
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("50.25", "The Price of the item is: 50.25 EUR")]
        [TestCase(XtraxConst.NOT_FOUND, "  xxxxxxxxx   ")]
        public void GetOutputChained_WhenContentIsThere_ThenFound(string expectedOutput, string content)
        {
            _sut = new BetweenXtrax("The Price of the item is:", "EUR");
            var rump = DataTruck.CreateText(content);

            var data = _sut.GetOutputChained(rump);

            Assert.AreEqual(expectedOutput, data.GetText());
        }

        [Test]
        public void GetOutputChained_WhenContentIsInTitle_ThenFound()
        {
            string expectedOutput = "THE TITLE";
            string content = "<html class=\"no-touch csst\" style=\"\" data-whatclasses=\"global - modal\" data-whatinput=\"mouse\" lang=\"en\"><head><link> " +
                "<title>  THE TITLE  </title>";
            _sut = new BetweenXtrax("<title>", "</title>");
            var rump = DataTruck.CreateText(content);

            var data = _sut.GetOutputChained(rump);

            Assert.AreEqual(expectedOutput, data.GetText());
        }
    }
}
