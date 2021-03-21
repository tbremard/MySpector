namespace MySpector
{
    public class HeaderEntry
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public HeaderEntry(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

}
