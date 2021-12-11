
namespace LinkCrawler.Domain.Utils.Extensions;

public static class StringExtensions
{
    public static bool StartsWithIgnoreCase(this string str, string startsWith)
    {
        if (string.IsNullOrEmpty(str) && string.IsNullOrEmpty(startsWith))
            return true;

        if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(startsWith))
            return false;

        return str.StartsWith(startsWith, true, CultureInfo.InvariantCulture);
    }
    public static bool ToBool(this string str)
    {
        bool parsed;
        bool.TryParse(str, out parsed);
        return parsed;
    }

    public static string TrimEnd(this string input, string suffixToRemove)
    {
        if (input != null && suffixToRemove != null
          && input.EndsWith(suffixToRemove))
        {
            return input.Substring(0, input.Length - suffixToRemove.Length);
        }
        return input;
    }
}
