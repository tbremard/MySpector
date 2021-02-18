using System.Text.Json;

namespace MySpector.Objects
{
    public class Jsoner
    {
        public static string Empty => string.Empty;

        public static T FromJson<T>(string json)
        {
            var ret = JsonSerializer.Deserialize<T>(json);
            return ret;
        }

        public static string ToJson<T>(T x)
        {
            string ret = JsonSerializer.Serialize(x);
            return ret;
        }
    }
}