﻿using NLog;

namespace MySpector.Objects
{
    public class NumberIsDifferentChecker : IChecker
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        decimal Reference;

        public NumberIsDifferentChecker(decimal reference)
        {
            Reference = reference;
        }

        public bool Check(IDataTruck input)
        {
            bool ret;
            if (input == null)
                return false;
            decimal? number = input.GetNumber();
            if (!number.HasValue)
            {
                _log.Error("Invalid number (did you miss TextToNumber?)");
                return false;
            }
            decimal Sample = number.Value;
            ret = Sample != Reference;
            _log.Debug($"{Sample} != {Reference}: {ret}");
            return ret;
        }
    }
}
