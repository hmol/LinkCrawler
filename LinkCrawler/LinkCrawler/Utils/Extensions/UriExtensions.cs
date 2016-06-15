using System;
using System.Linq;

namespace LinkCrawler.Utils.Extensions
{
    public static class UriExtensions
    {
        public static string RemoveSegments(this Uri uri)
        {
            var uriString = uri.ToString();
            var segments = string.Join("/", uri.Segments.Where(x => x != "/"));

            return uriString.TrimEnd(segments);
        }
    }
}
