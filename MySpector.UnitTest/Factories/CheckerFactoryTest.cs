using NUnit.Framework;
using NLog;
using MySpector.Objects;

namespace MySpector.UnitTest
{
    public class CheckerFactoryTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        [TestCase(CheckerType.NumberIsLess, "{\"Reference\":1200, \"OrEqual\":true}")]
        [TestCase(CheckerType.NumberIsDifferent, "{\"Reference\":1200, \"OrEqual\":true}")]
        [TestCase(CheckerType.NumberIsEqual, "{\"Reference\":1200, \"OrEqual\":true}")]
        [TestCase(CheckerType.NumberIsGreater, "{\"Reference\":1200, \"OrEqual\":true}")]
        [TestCase(CheckerType.TextDoContain, "{\"Token\":\"my token\", \"IgnoreCase\":true}")]
        [TestCase(CheckerType.TextDoNotContain, "{\"Token\":\"my token\", \"IgnoreCase\":true}")]
        public void Create_WenCheckerTypeIsValid_ThenOk(CheckerType type, string arg)
        {
            var param = new CheckerParam(type , arg);

            var checker = CheckerFactory.Create(param);

            Assert.IsNotNull(checker);
        }
    }
}