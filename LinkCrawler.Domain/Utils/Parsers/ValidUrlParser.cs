
namespace LinkCrawler.Domain.Utils.Parsers;

public class ValidUrlParser : IValidUrlParser
{
    public ValidUrlParser(ISettings settings)
    {
        Regex = new Regex(settings.ValidUrlRegex);
        var baseUri = new Uri(settings.BaseUrl);
        BaseUrl = baseUri.RemoveSegments();
    }

    public bool Parse(string url, out string validUrl)
    {
        validUrl = string.Empty;

        if (string.IsNullOrEmpty(url))
            return false;

        if(url.Contains('#'))  url = url.Remove(url.LastIndexOf('#') );

        if (Regex.IsNotMatch(url)
            || !Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri? parsedUri))
            return false;

        if (parsedUri.IsAbsoluteUri)
        {
            //validUrl = url;
            return false;
        }
        if (url.StartsWith("//"))
        {
            var newUrl = string.Concat("http:", url);
            validUrl = newUrl;
            return true;
        }
        if (url.StartsWith("/"))
        {
            var newUrl = string.Concat(BaseUrl, url);
            validUrl = newUrl;
            return true;
        }
        return false;
    }

    public string BaseUrl { get; set; }
    public Regex Regex { get; set; }
}
