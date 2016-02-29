using LinkCrawler.Utils.Extensions;
using LinkCrawler.Utils.Settings;
using System;
using System.Text.RegularExpressions;

namespace LinkCrawler.Utils.Parsers
{
    public class ValidUrlParser : IValidUrlParser
    {
        public Regex Regex { get; set; }
        public string BaseUrl { get; set; }
        public ValidUrlParser(ISettings settings)
        {
            Regex = new Regex(settings.ValidUrlRegex);
            BaseUrl = settings.BaseUrl;
        }

        public bool Parse(string url, out string validUrl)
        {
            validUrl = string.Empty;

            if (string.IsNullOrEmpty(url))
                return false;

            Uri parsedUri;

            if (Regex.IsNotMatch(url)
                || !Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out parsedUri))
                return false;

            if (parsedUri.IsAbsoluteUri)
            {
                validUrl = url;
                return true;
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
    }
}
