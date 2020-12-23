﻿using NSubstitute;
using NUnit.Framework;
using MySpector.Core;

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
            var stubNotifier = Substitute.For<Notify>();
            var sample = TestSampleFactory.CreateSample(TestSampleId.PS4_SATURN);
            var transformer = new TextToNumberXtrax();
            sample.Rule.SetNext(transformer);
            const decimal TARGET_PRICE = 329.52m;
            var checker = new NumberIsEqualChecker(TARGET_PRICE);
            HttpTarget target = null;
            var item = new WatchItem(sample.Name, target, true, sample.Rule, checker, stubNotifier);
            var sut = new SpectorPipeline(item);

            bool isOk = sut.Process();

            Assert.IsTrue(isOk);
            stubNotifier.Received().NotifyChained(Arg.Any<string>());
        }
    }
}
