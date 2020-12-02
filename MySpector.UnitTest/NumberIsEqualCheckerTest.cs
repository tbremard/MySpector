using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class NumberIsEqualCheckerTest
    {
        NumberIsEqualChecker _sut;
        [SetUp]
        public void Setup()
        {

        }

        [TestCase(true, 0, 0)]
        [TestCase(true, 0.0001, 0.0001)]
        [TestCase(false, 4, 5)]
        [TestCase(true, 5, 5)]
        [TestCase(false, 6, 5)]
        [TestCase(false, -6, -5)]
        [TestCase(true, -5, -5)]
        [TestCase(true, -5.56, -5.56)]
        [TestCase(false, -4.1, -5.1)]
        public void Check_WenInputIsValid_ThenOk(bool expectedOutput, double sample, double reference)
        {
            _sut = new NumberIsEqualChecker(new decimal(sample), new decimal(reference));

            var data = _sut.Check();

            Assert.AreEqual(expectedOutput, data);
        }
    }

}
