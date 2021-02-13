namespace MySpector.Objects
{
    public static class Escaper
    {
        public static string EscapeDoubleQuotes(string rawString)
        {
            string ret = rawString.Replace("\"", "\\\"");
            return ret;
        }
    }
}


