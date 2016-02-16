using System;
using System.Globalization;
using LinkCrawler.Utils.Extensions;

namespace LinkCrawler.Utils
{
    public static class ValidUrl
    {
        public static bool Parse(string url, string baseUrl, out string validUrl)
        {
            validUrl = string.Empty;

            Uri parsedUri;
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out parsedUri)
                || url.StartsWithIgnoreCase(Constants.Html.Mailto)
                || url.StartsWithIgnoreCase(Constants.Html.Tel)
                || url.StartsWithIgnoreCase(Constants.Html.Javascript)
                || url.StartsWithIgnoreCase(Constants.Html.Sms))
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
                var newUrl = string.Concat(baseUrl, url);
                validUrl = newUrl;
                return true;
            }
            return false;
        }
    }
}
