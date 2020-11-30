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
            TestSampleFactory.Setup();
        }

        [TestCase(TestSampleId.ZOTAC_EN72070V_GALAXUS)]
        [TestCase(TestSampleId.ZOTAC_EN72070V_GALAXUS_FULL_PAGE)]
        [TestCase(TestSampleId.PS4_SATURN)]
        [TestCase(TestSampleId.PS4_SATURN_FULL_PAGE)]
        public void ExtractData_WhenXpathIsValidAndContentIsThere_ThenFound(TestSampleId sampleId)
        {
            var sample = TestSampleFactory.CreateSample(sampleId);

            var data = _sut.ExtractData(sample.Rump, sample.Rule);

            Assert.AreEqual(sample.ExpectedOutput, data.Value);
            Assert.AreEqual(sample.ExpectedOutput, data.Value);
        }
    }
}
