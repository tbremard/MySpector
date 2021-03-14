using MySpector.Objects;
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
            var arg = new ComparaisonArg(new decimal(reference), true);
            _sut = new NumberIsEqualChecker(arg);
            var input = new DataTruck(null, new decimal(sample));

            var data = _sut.Check(input);

            Assert.AreEqual(expectedOutput, data);
        }
    }
}
