using NUnit.Framework;

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

        [TestCase("1189,99", TestSampleId.ZOTAC_EN72070V_GALAXUS)]
       // [TestCase("329", TestSamples.PS4_MEDIAMARK)]
        public void ExtractData_WhenContentIsThere_ThenFound(string expected, TestSampleId sampleId)
        {
            var sample = TestSampleFactory.CreateSample(sampleId);

            var data = _sut.ExtractData(sample.Rump, sample.Rule);

            Assert.AreEqual(expected, data.Value);
        }
    }
}
