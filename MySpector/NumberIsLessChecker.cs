﻿namespace MySpector
{
    public class NumberIsLessChecker : IChecker
    {
        decimal Sample;
        decimal Reference;
        bool OrEqual;

        public NumberIsLessChecker(decimal reference, bool orEqual)
        {
            Reference = reference;
            OrEqual = orEqual;
        }

        public bool Check(IDataTruck input)
        {
            bool ret;
            if (input == null)
                return false;
            decimal? number = input.GetNumber();
            if (!number.HasValue)
            {
                return false;
            }
            Sample = number.Value;
            if (OrEqual)
            {
                ret = Sample <= Reference;
            }
            else
            {
                ret = Sample < Reference;
            }
            return ret;
        }
    }
}
