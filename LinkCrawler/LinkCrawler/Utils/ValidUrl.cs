using System;

namespace LinkCrawler.Utils
{
    public static class ValidUrl
    {
        public static bool Parse(string url, string baseUrl, out string validUrl)
        {
            validUrl = string.Empty;

            Uri parsedUri;
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out parsedUri)
                || url.StartsWith(Constants.Html.Mailto)
                || url.StartsWith(Constants.Html.Tel))
                return false;

            if (parsedUri.IsAbsoluteUri)
            {
                validUrl = url;
                return true;
            }
            else if (url.StartsWith("//"))
            {
                var newUrl = string.Concat("http:", url);
                validUrl = newUrl;
                return true;
            }
            else if (url.StartsWith("/"))
            {
                var newUrl = string.Concat(baseUrl, url);
                validUrl = newUrl;
                return true;
            }
            return false;
        }
    }
}
