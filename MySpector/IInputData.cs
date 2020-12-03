namespace MySpector
{
    public interface IInputData
    {
        public string GetText();
        public decimal? GetNumber();
    }

    public class InputData : IInputData
    {
        decimal? _number;
        string _text;

        public InputData(string text, decimal? number)
        {
            _text = text;
            _number = number;
        }

        public InputData(string text)
        {
            _text = text;
            _number = null;
        }

        public decimal? GetNumber()
        {
            return _number;
        }

        public string GetText()
        {
            return _text;
        }

        public static IInputData CreateText(string text)
        {
            IInputData ret = new InputData(text);
            return ret;
        }
    }
}
