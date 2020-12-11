using NUnit.Framework;
using System.Net;
using NLog;
using MySpector.Core;

namespace MySpector.UnitTest
{
    public class DownloaderTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        Downloader _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Downloader();
        }

        [Test]
        public void HttpRequest_WenInputIsValid_ThenOk()
        {
            var httpTarget = new HttpTarget(TestSampleFactory.ZOTAC_EN72070V_GALAXUS_FULL_PAGE.Url);

            var data = _sut.HttpRequest(httpTarget);

            Assert.AreEqual(HttpStatusCode.OK, data.HttpResponseCode);
        }

        [Test]
        [Ignore("SSL certificate by WebClient is an issue")]
        public void HttpRequest_WenServerSpotRobot_ThenOk()
        {
            _log.Debug("xxxxxxxxxxxxx");
            var httpTarget = new HttpTarget(TestSampleFactory.PS4_SATURN_FULL_PAGE.Url);

            var data = _sut.HttpRequest(httpTarget);

            Assert.AreEqual(HttpStatusCode.OK, data.HttpResponseCode);
        }
    }




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
        public void Create_WenInputIsValid_ThenOk()
        {
            const string arg = "1200";
            var param = new CheckerParam(CheckerType.IsLess, arg);
            var checker = CheckerFactory.Create(param);
            var number = DataTruck.CreateNumber(900m);

            bool ret = checker.Check(number);

            Assert.IsTrue(ret);
        }
    }
}