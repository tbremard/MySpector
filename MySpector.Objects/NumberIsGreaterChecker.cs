using NLog;

namespace MySpector.Objects
{
    public class NumberIsGreaterChecker : IChecker// << need JsonArg for DB insertion
    {
        public int? DbId { get; set; }
        public CheckerType Type => CheckerType.NumberIsGreater;
        public string JsonArg { get; }
        static Logger _log = LogManager.GetCurrentClassLogger();
        decimal Reference;
        bool OrEqual;

        public NumberIsGreaterChecker(decimal reference, bool orEqual)// <<<<<  Need 2 parameters
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
