using NLog;

namespace MySpector.Objects
{
    public class NumberIsEqualChecker : IChecker
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        public int? DbId { get; set; }
        public CheckerType Type => CheckerType.NumberIsEqual;
        public string JsonArg { get; }

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
            decimal Sample = number.Value;
            ret = Sample == Reference;
            _log.Debug($"{Sample} == {Reference}: {ret}");
            return ret;
        }
    }
}
