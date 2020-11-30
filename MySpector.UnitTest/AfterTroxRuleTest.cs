﻿using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class AfterTroxRuleTest
    {
        AfterTroxRule _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new AfterTroxRule("The Price of the item is:");
        }

        [TestCase("50.25", "The Price of the item is:    50.25   ")]
        [TestCase(TroxRuleConst.NOT_FOUND, "  xxxxxxxxx   ")]
        public void ExtractData_WhenContentIsThere_ThenFound(string expectedOutput, string content)
        {
            var rump = new Rump(content);

            var data = _sut.GetOutput(rump);

            Assert.AreEqual(expectedOutput, data);
        }
    }
}