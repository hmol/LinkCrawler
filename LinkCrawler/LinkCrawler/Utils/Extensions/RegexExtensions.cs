using System.Text.RegularExpressions;

namespace LinkCrawler.Utils.Extensions
{
    public static class RegexExtensions
    {
        public static bool IsNotMatch(this Regex regex, string str)
        {
            return !regex.IsMatch(str);
        }
    }
}
