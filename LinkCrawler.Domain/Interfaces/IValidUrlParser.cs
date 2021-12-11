
namespace LinkCrawler.Domain.Interfaces;

public interface IValidUrlParser
{
    Regex Regex { get; set; }
    string BaseUrl { get; set; }
    bool Parse(string url, out string validUrl);
}
