﻿using NUnit.Framework;
using NLog;
using MySpector.Core;
using MySpector.Objects;

namespace MySpector.UnitTest
{
    public class SqlGrabberTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        SqlGrabber _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new SqlGrabber();
        }

        [Test]
        public void Grab_WenInputIsValid_ThenOk()
        {
            string connectionString = "";
            string sqlQuery = "";
            string provider = "";
            var target = new SqlTarget(connectionString, sqlQuery, provider);
            Trox item = new Trox("test", true, target, null, null, null, null);

            var data = _sut.Grab(target);

            Assert.IsTrue(data.Success);
        }
    }
}