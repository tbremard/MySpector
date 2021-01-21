﻿namespace MySpector.Objects
{
    public class CheckerParam
    {
        public CheckerType Type { get; }
        public string Arg { get; }

        public CheckerParam(CheckerType type, string arg)
        {
            Type = type;
            Arg = arg;
        }
    }
}