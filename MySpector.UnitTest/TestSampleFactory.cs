﻿using System;
using System.Collections.Generic;
using System.IO;

namespace MySpector.UnitTest
{
    internal class TestSampleFactory
    {
        static Dictionary<TestSampleId, TestDescriptor> samples;

        public static void Setup()
        {
            if (samples != null)
            {
                return;
            }
            samples = new Dictionary<TestSampleId, TestDescriptor>();
            samples.Add(TestSampleId.PS4_SATURN, PS4_SATURN);
            samples.Add(TestSampleId.PS4_SATURN_FULL_PAGE, PS4_SATURN_FULL_PAGE);
            samples.Add(TestSampleId.ZOTAC_EN72070V_GALAXUS, ZOTAC_EN72070V_GALAXUS);
            samples.Add(TestSampleId.ZOTAC_EN72070V_GALAXUS_FULL_PAGE, ZOTAC_EN72070V_GALAXUS_FULL_PAGE);
            samples.Add(TestSampleId.BDIY_FULL_PAGE, BDIY_FULL_PAGE);
        }

        static TestDescriptor PS4_SATURN = new TestDescriptor()
        {
            Html = "<html><body>"+
                   "<span display=\"inline-block\" font-size=\"xxxxxl\" font-family=\"price\" class=\"Typ bWz Pr bU\">329.<sup font-size=\"xxxl\" font-family=\"price\" class=\"Typo0 gmP Pried_0 b\">52</sup></span>"+
                   "</body></html>",
            Xpath = "/html/body/span",
            Url = "https://www.saturn.de/de/product/_sony-playstation-4-pro-1tb-jet-black-g-eur-2495539.html",
            ExpectedOutput = "329.52"
        };
        public static TestDescriptor PS4_SATURN_FULL_PAGE = new TestDescriptor()
        {
            Html = File.ReadAllText("samples_html\\ps4pro_saturn.html"),
            Xpath = "/html/body/div[1]/div[2]/div[2]/div[1]/div/div[4]/div/div/div[1]/div/div[1]/div/div/div/div[2]/div[2]/span[2]",
            Url = "https://www.saturn.de/de/product/_sony-playstation-4-pro-1tb-jet-black-g-eur-2495539.html",
            ExpectedOutput = "329.52"
        };
        static TestDescriptor ZOTAC_EN72070V_GALAXUS = new TestDescriptor()
        {
            Html = "<html><head></head><body><div class=\"Z1h2\"><strong class=\"ZZ9v\"> 1189,99</strong></div></body></html>",
            Xpath = "/html/body/div/strong",
            Url = "https://www.galaxus.de/de/s1/product/zotac-zbox-magnus-en72070v-intel-core-i7-9750h-0gb-pc-13590721",
            ExpectedOutput = "1189,99"
        };
        public static TestDescriptor ZOTAC_EN72070V_GALAXUS_FULL_PAGE = new TestDescriptor()
        {
            Html = File.ReadAllText("samples_html\\Zotac_Galaxus.html"),
            Xpath = "/html/body/div[1]/div/div[2]/div[1]/main/div/div[2]/div/div[2]/div/div[1]/strong",
            Url = "https://www.galaxus.de/de/s1/product/zotac-zbox-magnus-en72070v-intel-core-i7-9750h-0gb-pc-13590721",
            ExpectedOutput = "1189,99"
        };
        private static TestDescriptor BDIY_FULL_PAGE = new TestDescriptor()
        {
            Html = File.ReadAllText("samples_html\\BDIY.html"),
            Xpath = "/html/body/div[6]/div/div/section[2]/div[1]/div[2]/section[1]/section/section[2]/section/div[1]",
            Url = "https://www.bloomberg.com/quote/BDIY:IND?sref=GKjIETf1",
            ExpectedOutput = "1,227.00USD"
        };

        internal static TestSample CreateSample(TestSampleId sampleId)
        {
            TestSample ret;
            if (!samples.ContainsKey(sampleId))
            {
                throw new ArgumentOutOfRangeException("Dictionnary do not contain this test");
            }
            TestDescriptor selected = samples[sampleId];
            ret = new TestSample(selected.Html, selected.Xpath, selected.ExpectedOutput);
            return ret;
        }
    }
}
