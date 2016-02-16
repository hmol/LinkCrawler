using System;
using System.Globalization;

namespace LinkCrawler.Utils.Extensions
{
    public static class StringExtensions
    {
        public static bool StartsWithIgnoreCase(this string str, string startsWith)
        {
            return str.StartsWith(startsWith, true, CultureInfo.InvariantCulture);
        }
        public static bool ToBool(this string str)
        {
            var parsed = false;
            Boolean.TryParse(str, out parsed);
            return parsed;
        }
    }
}
