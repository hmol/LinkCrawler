
namespace LinkCrawler.Domain.Interfaces;
public interface IResponseModel
{
    string Markup { get; }
    string RequestedUrl { get; }
    string ReferrerUrl { get; }
    HttpStatusCode StatusCode { get; }
    int StatusCodeNumber { get; }
    bool IsSuccess { get; }
    bool ShouldCrawl { get; }
    string ToString();
}
