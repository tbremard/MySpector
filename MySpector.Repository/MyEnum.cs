using System;

namespace MySpector.Repo
{
    public static class MyEnum
    {
        public static T Parse<T>(string value)
        {
            var ret = (T)Enum.Parse(typeof(T), value, true);
            return ret;
        }

    }
}
