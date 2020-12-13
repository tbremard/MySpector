using NLog;

namespace MySpector
{
    public class NumberIsLessChecker : IChecker
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
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
                _log.Error("invalid number");
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
            _log.Debug($"{Sample} < {Reference}: {ret}");
            return ret;
        }
    }
}
