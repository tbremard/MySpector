using System;
using System.IO;

namespace MySpector
{
    public interface IDataTruck
    {
        public string GetText();
        public decimal? GetNumber();
        string PreviewText { get; }
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

        public DataTruck(decimal? number)
        {
            _text = null;
            _number = number;
        }

        public decimal? GetNumber()
        {
            return _number;
        }

        public string GetText()
        {
            return _text;
        }

        public string PreviewText
        {
            get
            {
                int len = Math.Min(20, _text.Length);
                string ret = _text.Substring(0, len) + "...";
                return ret;
            }
        }

        public static IDataTruck CreateNumber(decimal? number)
        {
            IDataTruck ret = new DataTruck(number);
            return ret;
        }

        public static IDataTruck CreateText(string text)
        {
            IDataTruck ret = new DataTruck(text);
            return ret;
        }

        public static IDataTruck CreateTextFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            string content = File.ReadAllText(filePath);
            IDataTruck ret = new DataTruck(content);
            return ret;
        }
    }
}
