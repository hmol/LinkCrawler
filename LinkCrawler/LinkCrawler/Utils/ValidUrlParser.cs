using System;
using System.Text.RegularExpressions;
using LinkCrawler.Utils.Extensions;

namespace LinkCrawler.Utils
{
    public class ValidUrlParser
    {
        public Regex Regex;
        public string BaseUrl;
        public ValidUrlParser()
        {
            Regex = new Regex(Settings.Instance.NotValidUrlRegex);
            BaseUrl = Settings.Instance.BaseUrl;
        }

        public bool Parse(string url, out string validUrl)
        {
            validUrl = string.Empty;

            Uri parsedUri;
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out parsedUri)
                    || Regex.IsMatch(url))
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
