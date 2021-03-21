using NUnit.Framework;
using NLog;
using MySpector.Objects;

namespace MySpector.UnitTest
{
    public class HttpGrabberTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        HttpGrabber _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new HttpGrabber();
        }

        [Test]
        public void Grab_WenInputIsValid_ThenOk()
        {
            var httpTarget = new HttpTarget(TestSampleFactory.ZOTAC_EN72070V_GALAXUS_FULL_PAGE.Url);

            var data = _sut.Grab(httpTarget);

            Assert.IsTrue(data.Success);
        }

        [Test]
        [Ignore("SSL certificate by WebClient is an issue")]
        public void Grab_WenServerSpotRobot_ThenOk()
        {
            _log.Debug("xxxxxxxxxxxxx");
            var httpTarget = new HttpTarget(TestSampleFactory.PS4_SATURN_FULL_PAGE.Url);

            var data = _sut.Grab(httpTarget);

            Assert.IsTrue(data.Success);
        }
    }
}