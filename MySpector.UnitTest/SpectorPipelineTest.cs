using NSubstitute;
using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class SpectorPipelineTest
    {

        [SetUp]
        public void Setup()
        {
            TestSampleFactory.Setup();
        }

        [Test]
        public void Process_WhenPriceIsEqualToTarget_ThenCheckNotificationIsTriggered()
        {
            var stubNotifier = Substitute.For<INotifier>();
            var sample = TestSampleFactory.CreateSample(TestSampleId.PS4_SATURN);
            var transformer = new TextToNumberXtraxRule();
            sample.Rule.SetNext(transformer);
            const decimal TARGET_PRICE = 329.52m;
            var checker = new NumberIsEqualChecker(TARGET_PRICE);
            var sut = new SpectorPipeline(sample.Name, sample.Data, sample.Rule, checker, stubNotifier);

            bool isOk = sut.Process();

            Assert.IsTrue(isOk);
            stubNotifier.Received().Notify(Arg.Any<string>());
        }
    }
}
