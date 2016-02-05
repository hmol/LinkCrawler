using System;

namespace LinkCrawler.Utils.Extensions
{
    public static class StringExtensions
    {
        public static bool ToBool(this string str)
        {
            var parsed = false;
            Boolean.TryParse(str, out parsed);
            return parsed;
        }
    }
}
