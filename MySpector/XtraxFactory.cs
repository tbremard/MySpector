﻿using MySpector.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;

namespace MySpector
{
    public class AfterArg
    {
        public string Prefix { get; set; }
    }

    
    public class XpathArg
    {
        public string Path { get; set; }
    }

    public class XtraxFactory
    {

        public static XtraxRule Create(XtraxDefinition param)
        {
            XtraxRule ret=null;
            switch (param.XtraxType)
            {
                case XtraxType.Xpath:
                    var xpathArg = JsonSerializer.Deserialize<XpathArg>(param.Arg);
                    ret = new XpathXtraxRule(xpathArg.Path);
                    break;
                case XtraxType.After:
                    var arg = JsonSerializer.Deserialize<AfterArg>(param.Arg);
                    ret = new AfterXtraxRule(arg.Prefix);
                    break;
                default:
                    throw new InvalidEnumArgumentException("unknown XtraxType: "+param.XtraxType);
            }
            return ret;
        }

        public static XtraxRule CreateChain(List<XtraxDefinition> xTraxParams)
        {
            if (xTraxParams.Count == 0)
                return new EmptyXtraxRule();
            XtraxRule ret = Create(xTraxParams[0]);
            for (int i=1; i< xTraxParams.Count; i++)
            {
                XtraxRule next = Create(xTraxParams[i]);
                ret.SetNext(next);
            }
            return ret;
        }
    }
}