﻿using NUnit.Framework;
using System.Net;

namespace MySpector.UnitTest
{
    public class DownloaderTest
    {
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
        public void HttpRequest_WenServerSpotRobot_ThenOk()
        {
            System.Console.WriteLine("xxxxxxxxxxxxx");
            var httpTarget = new HttpTarget(TestSampleFactory.PS4_SATURN_FULL_PAGE.Url);

            var data = _sut.HttpRequest(httpTarget);

            Assert.AreEqual(HttpStatusCode.OK, data.HttpResponseCode);
        }
    }
}
