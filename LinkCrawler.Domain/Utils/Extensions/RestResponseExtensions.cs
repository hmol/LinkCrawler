
namespace LinkCrawler.Domain.Utils.Extensions;

public static class RestResponseExtensions
{
    public static bool IsHtmlDocument(this RestResponse restResponse)
    {
        return restResponse?.ContentType?.StartsWith(Constants.Response.ContentTypeTextHtml)??false;
    }
}
