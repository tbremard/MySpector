using NUnit.Framework;
using NLog;
using MySpector.Objects;

namespace MySpector.UnitTest
{
    public class CheckerFactoryTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        //Downloader _sut;
        //[SetUp]
        //public void Setup()
        //{
        //    _sut = new Downloader();
        //}

        [Test]
        public void Create_WenCheckerTypeIsLess_ThenCheckIsSucess()
        {
            const string arg = "{\"Reference\":1200, \"OrEqual\":true}";
            var param = new CheckerParam(CheckerType.IsLess, arg);

            var checker = CheckerFactory.Create(param);

            var number = DataTruck.CreateNumber(900m);
            bool ret = checker.Check(number);
            Assert.IsTrue(ret);
        }
    }
}