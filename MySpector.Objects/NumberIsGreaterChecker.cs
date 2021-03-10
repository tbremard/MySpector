using NLog;
using System;

namespace MySpector.Objects
{
    public class NumberIsGreaterChecker : IChecker// << need JsonArg for DB insertion
    {
        public int? DbId { get; set; }
        public CheckerType Type => CheckerType.NumberIsGreater;
        public string JsonArg { get { return Jsoner.ToJson(_arg); } }
        static Logger _log = LogManager.GetCurrentClassLogger();
        ComparaisonArg _arg;

        public NumberIsGreaterChecker(ComparaisonArg arg)
        {
            _arg = arg;
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
            if (_arg.OrEqual)
            {
                ret = Sample >= _arg.Reference;
            }
            else
            {
                ret = Sample > _arg.Reference;
            }
            _log.Debug($"{Sample} > {_arg.Reference}: {ret}");
            return ret;
        }

        public override string ToString()
        {
            string ret = GetType().Name + " " + _arg.Reference;
            return ret;
        }

    }
}
