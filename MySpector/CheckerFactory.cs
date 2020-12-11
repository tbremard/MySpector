using System.ComponentModel;

namespace MySpector.Core
{
    public class CheckerFactory
    {
        public static IChecker Create(CheckerParam param)
        {
            IChecker ret;
            switch (param.CheckerType)
            {
                case CheckerType.IsLess:
                    decimal reference = 1m;
                    bool orEqual = true;
                    ret = new NumberIsLessChecker(reference, orEqual);
                    break;
                default:
                    throw new InvalidEnumArgumentException("checker type is not handled");
            }
            return ret;
        }
    }
}