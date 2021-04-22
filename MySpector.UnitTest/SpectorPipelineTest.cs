using NSubstitute;
using NUnit.Framework;
using MySpector.Core;
using MySpector.Objects;
using System;

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
            var arg = new ComparaisonArg(TARGET_PRICE, true);
            var checker = new NumberIsEqualChecker(arg);
            HttpTarget target = new HttpTarget("FAKE URI");
            var item = new Trox(sample.Name, true, target, sample.Rule, checker, stubNotifier, null);
            item.Grabber = new StubDownloader(sample.Data);
            var sut = new SpectorPipeline(item);

            bool isOk = sut.Process();

            Assert.IsTrue(isOk);
            stubNotifier.Received().NotifyChained(Arg.Any<string>());
        }

        [Test]
        public void Process_WhenLatencyIsAboveLimit_ThenLatencyErrorIsTriggered()
        {
            var stubNotifier = Substitute.For<Notifier>();
            var stubNotifierError = Substitute.For<Notifier>();
            var sample = TestSampleFactory.CreateSample(TestSampleId.PS4_SATURN);
            var transformer = new TextToNumberXtrax();
            sample.Rule.SetNext(transformer);
            const decimal TARGET_PRICE = 329.52m;
            var arg = new ComparaisonArg(TARGET_PRICE, true);
            var checker = new NumberIsEqualChecker(arg);
            HttpTarget target = new HttpTarget("FAKE URI");
            var trox = new Trox(sample.Name, true, target, sample.Rule, checker, stubNotifier, stubNotifierError);
            trox.MaxLatency = new TimeSpan(0, 1, 0);
            var stubGrabber = Substitute.For<IGrabber>();
            var highLatency = new GrabResponse(sample.Data.GetText(), true, new TimeSpan(1, 0, 0), null);
            stubGrabber.Grab(Arg.Any<IGrabTarget>()).Returns(highLatency);
            trox.Grabber = stubGrabber;
            var sut = new SpectorPipeline(trox);

            bool isOk = sut.Process();

            Assert.IsTrue(isOk);
            stubNotifier.Received().NotifyChained(Arg.Any<string>());
            stubNotifierError.Received().NotifyChained(Arg.Any<string>());
        }
    }
}
