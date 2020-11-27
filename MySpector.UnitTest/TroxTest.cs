using NUnit.Framework;
using System.Data;

namespace MySpector.UnitTest
{
    public class TroxTest
    {
        Trox _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Trox();
        }

        [Test]
        public void ExtractData_WhenContentIsThere_ThenFound()
        {
            var rump = CreateLocalRump();
            var rule = CreateLocaleRule();

            var data = _sut.ExtractData(rump, rule);

            const string expected = "100";
            Assert.AreEqual(expected, data.Value);

        }

        private Rule CreateLocaleRule()
        {
            var ret = new Rule();
            return ret;
        }

        private Rump CreateLocalRump()
        {
            var ret = new Rump();
            return ret;
        }
    }
}
