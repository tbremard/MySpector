using NSubstitute;
using NUnit.Framework;
using MySpector.Core;
using MySpector.Objects;

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
            var stubNotifier = Substitute.For<Notifier>();
            var sample = TestSampleFactory.CreateSample(TestSampleId.PS4_SATURN);
            var transformer = new TextToNumberXtrax();
            sample.Rule.SetNext(transformer);
            const decimal TARGET_PRICE = 329.52m;
            var checker = new NumberIsEqualChecker(TARGET_PRICE);
            HttpTarget target = new HttpTarget("FAKE URI");
            var item = new Trox(sample.Name, true, target, sample.Rule, checker, stubNotifier);
            item.Downloader = new StubDownloader(sample.Data);
            var sut = new SpectorPipeline(item);

            bool isOk = sut.Process();

            Assert.IsTrue(isOk);
            stubNotifier.Received().NotifyChained(Arg.Any<string>());
        }
    }
}
