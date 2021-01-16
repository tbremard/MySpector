using NUnit.Framework;
using NLog;
using MySpector.Objects;

namespace MySpector.UnitTest
{
    public class HttpDownloaderTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        HttpDownloader _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new HttpDownloader();
        }

        [Test]
        public void Download_WenInputIsValid_ThenOk()
        {
            var httpTarget = new HttpTarget(TestSampleFactory.ZOTAC_EN72070V_GALAXUS_FULL_PAGE.Url);
            Trox item = new Trox("test", httpTarget, true, null, null, null);

            var data = _sut.Download(item);

            Assert.IsTrue(data.Success);
        }

        [Test]
        [Ignore("SSL certificate by WebClient is an issue")]
        public void Download_WenServerSpotRobot_ThenOk()
        {
            _log.Debug("xxxxxxxxxxxxx");
            var httpTarget = new HttpTarget(TestSampleFactory.PS4_SATURN_FULL_PAGE.Url);
            Trox item = new Trox("test", httpTarget, true, null, null, null);

            var data = _sut.Download(item);

            Assert.IsTrue(data.Success);
        }
    }
}