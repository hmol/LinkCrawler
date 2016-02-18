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
            Regex = new Regex(Settings.Instance.ValidUrlRegex);
            BaseUrl = Settings.Instance.BaseUrl;
        }

        public bool Parse(string url, out string validUrl)
        {
            validUrl = string.Empty;

            if (string.IsNullOrEmpty(url))
                return false;
            
            Uri parsedUri;

            if (Regex.IsNotMatch(url)
                ||!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out parsedUri))
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
