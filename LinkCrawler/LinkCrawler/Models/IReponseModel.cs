using System.Net;

namespace LinkCrawler.Models
{
    public interface IResponseModel
    {
        string Markup { get; }
        string RequestedUrl { get; }
        string ReferrerUrl { get; }
        HttpStatusCode StatusCode { get; }
        int StatusCodeNumber { get; }
        bool IsSucess { get; }
        bool ShouldCrawl { get; }
        string ToString();
    }
}
