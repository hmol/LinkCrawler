using LinkCrawler.Utils.Extensions;
using LinkCrawler.Utils.Settings;
using System;
using System.Text.RegularExpressions;

namespace LinkCrawler.Utils.Parsers
{
    /// <summary>
    /// Parses a given text to validate if it is a valid url.
    /// 
    /// Some kinds of relative URLs are converted to absolute urls.
    /// You can count on either getting a valid absolute url from this
    /// class or getting a valid = false.
    /// </summary>
    public class ValidUrlParser : IValidUrlParser
    {
        public Regex Regex { get; set; }
        public string BaseUrl { get; set; }
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
