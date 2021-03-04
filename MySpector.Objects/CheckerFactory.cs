using System.ComponentModel;
using System.Text.Json;

namespace MySpector.Objects
{
    public class CheckerFactory
    {
        public static IChecker Create(CheckerParam param)
        {
            IChecker ret;
            switch (param.Type)
            {
                case CheckerType.IsLess:
                    var arg = JsonSerializer.Deserialize<ComparaisonArg>(param.Arg);
                    ret = new NumberIsLessChecker(arg.Reference, arg.OrEqual);
                    break;
                case CheckerType.IsGreater:
                    var argGreater = JsonSerializer.Deserialize<ComparaisonArg>(param.Arg);
                    ret = new NumberIsGreaterChecker(argGreater.Reference, argGreater.OrEqual);
                    break;
                case CheckerType.TextDoContain:
                    var argText = JsonSerializer.Deserialize<TextDoContainArg>(param.Arg);
                    ret = new TextDoContainChecker(argText);
                    break;
                default:
                    throw new InvalidEnumArgumentException($"CheckerType.{param.Type} is not handled by CheckerFactory");
            }
            return ret;
        }
    }
}