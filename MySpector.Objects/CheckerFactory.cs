using NLog;
using System.ComponentModel;
using System.Text.Json;

namespace MySpector.Objects
{
    public class CheckerFactory
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public static IChecker Create(CheckerParam param)
        {
            IChecker ret;
            switch (param.Type)
            {
                case CheckerType.NumberIsLess:
                    var argLess = JsonSerializer.Deserialize<ComparaisonArg>(param.Arg);
                    ret = new NumberIsLessChecker(argLess);
                    break;
                case CheckerType.NumberIsGreater:
                    var argGreater = JsonSerializer.Deserialize<ComparaisonArg>(param.Arg);
                    ret = new NumberIsGreaterChecker(argGreater);
                    break;
                case CheckerType.NumberIsEqual:
                    var argEqual = JsonSerializer.Deserialize<ComparaisonArg>(param.Arg);
                    ret = new NumberIsEqualChecker(argEqual);
                    break;

                case CheckerType.TextDoContain:
                    var argText = JsonSerializer.Deserialize<TextDoContainArg>(param.Arg);
                    ret = new TextDoContainChecker(argText);
                    break;
                default:
                    string message = $"CheckerType.{param.Type} is not handled by CheckerFactory";
                    _log.Error(message);
                    throw new InvalidEnumArgumentException(message);
            }
            ret.DbId = param.DbId;
            return ret;
        }
    }
}