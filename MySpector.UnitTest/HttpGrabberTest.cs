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

        [TestCase()]
        public void Grab_WenInputIsValid_ThenOk()
        {
            var httpTarget = new HttpTarget(TestSampleFactory.ZOTAC_EN72070V_GALAXUS_FULL_PAGE.Url);

            var data = _sut.Grab(httpTarget);

            Assert.IsTrue(data.Success);
        }

        [Test]
        public void Grab_WenForbidden_ThenNoSuccess()
        {
            var target = new HttpTarget(TestSampleFactory.PS4_SATURN_FULL_PAGE.Url); // spots robot => ask user to auth : HTTP 403 

            var data = _sut.Grab(target);

            Assert.IsFalse(data.Success, "Server should prevent to call this url");
        }

        [TestCase("https://allianz-fonds.webfg.net/sheet/fund/FR0013192572/730?date_entree=2018-04-04")]
        [TestCase("https://www.galaxus.de/de/s1/product/zotac-zbox-magnus-en72070v-intel-core-i7-9750h-0gb-pc-13590721")]
        [TestCase("https://www.hystou.com/Gaming-Mini-PC-F7-with-Nvidia-GeForce-GTX-1650-p177717.html")]
        //[TestCase("https://shop.westerndigital.com/de-de/products/internal-drives/wd-red-sata-2-5-ssd#WDS200T1R0A")] disabled because failing on Win7 because of OS restriction on cipher available on SSL
        public void Grab_WhenHttps_ThenOk(string url)
        {
            var target = new HttpTarget(url);

            var data = _sut.Grab(target);

            Assert.IsTrue(data.Success);
        }

        [Test]
        public void Grab_WenTimeoutIsReached_ThenKo()
        {
            string url = "https://allianz-fonds.webfg.net/sheet/fund/FR0013192572/730?date_entree=2018-04-04";
            var target = new HttpTarget(url);
            target.TimeoutMs = 1000;

            var data = _sut.Grab(target);

            _log.Debug("Latency: " + data.Latency.TotalMilliseconds + " ms");
            Assert.LessOrEqual(data.Latency.TotalMilliseconds, target.TimeoutMs);
            Assert.IsFalse(data.Success);
        }
    }
}