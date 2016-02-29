using System.Text.RegularExpressions;

namespace LinkCrawler.Utils.Parsers
{
    public interface IValidUrlParser
    {
        Regex Regex { get; set; }
        string BaseUrl { get; set; }
        bool Parse(string url, out string validUrl);
    }
}
