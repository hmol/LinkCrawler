
namespace LinkCrawler.Domain.Utils.Extensions;

public static class UriExtensions
{
    public static string RemoveSegments(this Uri uri)
    {
        var uriString = uri.ToString();
        var segments = string.Join(string.Empty, uri.Segments);
        return uriString.TrimEnd(segments);
    }
}
