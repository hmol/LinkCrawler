using RestSharp;

namespace LinkCrawler.Utils.Extensions
{
    public static class RestResponseExtensions
    {
        public static bool IsHtmlDocument(this IRestResponse restResponse)
        {
            return restResponse.ContentType.StartsWith(Constants.Response.ContentTypeTextHtml);
        }
    }
}
