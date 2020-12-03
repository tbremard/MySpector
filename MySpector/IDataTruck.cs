namespace MySpector
{
    public interface IDataTruck
    {
        public string GetText();
        public decimal? GetNumber();
    }

    public class DataTruck : IDataTruck
    {
        decimal? _number;
        string _text;

        public DataTruck(string text, decimal? number)
        {
            _text = text;
            _number = number;
        }

        public DataTruck(string text)
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

        public static IDataTruck CreateText(string text)
        {
            IDataTruck ret = new DataTruck(text);
            return ret;
        }
    }
}
