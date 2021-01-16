﻿using NLog;

namespace MySpector.Objects
{
    public class NumberIsEqualChecker : IChecker
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        decimal Sample;
        decimal Reference;

        public NumberIsEqualChecker(decimal reference)
        {
            Reference = reference;
        }

        public bool Check(IDataTruck input)
        {
            bool ret;
            if (input == null)
            {
                _log.Error("Invalid number (did you miss TextToNumber?)");
                return false;
            }
            decimal? number = input.GetNumber();
            if (!number.HasValue)
            {
                _log.Error("Invalid number (did you miss TextToNumber?)");
                return false;
            }
            Sample = number.Value;
            ret = Sample == Reference;
            _log.Debug($"{Sample} == {Reference}: {ret}");
            return ret;
        }
    }
}
