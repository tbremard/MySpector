using System.ComponentModel;
using System.Text.Json;

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
                    var arg = JsonSerializer.Deserialize<ComparaisonArg>(param.Arg);
                    ret = new NumberIsLessChecker(arg.Reference, arg.OrEqual);
                    break;
                case CheckerType.IsGreater:
                    var argGreater = JsonSerializer.Deserialize<ComparaisonArg>(param.Arg);
                    ret = new NumberIsGreaterChecker(argGreater.Reference, argGreater.OrEqual);
                    break;
                default:
                    throw new InvalidEnumArgumentException("checker type is not handled");
            }
            return ret;
        }
    }
}