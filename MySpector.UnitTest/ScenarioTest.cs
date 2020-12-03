using NSubstitute;
using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class ScenarioTest
    {
        [SetUp]
        public void Setup()
        {
            TestSampleFactory.Setup();
        }

        [Test]
        public void Process_WhenPriceIsBelowTarget_ThenCheckNotificationIsTriggered()
        {
            var stubNotifier = Substitute.For<INotifier>();
            var sample = TestSampleFactory.CreateSample(TestSampleId.PS4_SATURN);
            var checker = new NumberIsEqualChecker(329.52m);
            object transformer = null;
            var pipeline = new ExecutionPipeline(sample.Rump, sample.Rule, transformer, checker, stubNotifier);

            bool isOk = pipeline.Process();

            Assert.IsTrue(isOk);
            stubNotifier.Received().Notify(Arg.Any<string>());
        }
    }
}
