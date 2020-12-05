﻿using NUnit.Framework;
using System.Net;

namespace MySpector.UnitTest
{
    public class FileScraperTest
    {
        FileScraper _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new FileScraper();
        }

        [Test]
        public void Check_WenInputIsValid_ThenOk()
        {
            var httpTarget = new HttpTarget();
            httpTarget.Uri = TestSampleFactory.ZOTAC_EN72070V_GALAXUS_FULL_PAGE.Url;

            var data = _sut.HttpDownload(httpTarget);

            Assert.AreEqual(HttpStatusCode.OK, data.HttpResponseCode);
        }
    }
}
