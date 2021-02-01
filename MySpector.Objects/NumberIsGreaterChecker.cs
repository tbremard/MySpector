using NLog;

namespace MySpector.Objects
{
    public class NumberIsGreaterChecker : IChecker
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        decimal Reference;
        bool OrEqual;

        public NumberIsGreaterChecker(decimal reference, bool orEqual)
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
                _log.Error("Invalid number (did you miss TextToNumber?)");
                return false;
            }
            decimal Sample = number.Value;
            if (OrEqual)
            {
                ret = Sample >= Reference;
            }
            else
            {
                ret = Sample > Reference;
            }
            _log.Debug($"{Sample} > {Reference}: {ret}");
            return ret;
        }

        public override string ToString()
        {
            string ret = GetType().Name + " " + Reference;
            return ret;
        }

    }
}
