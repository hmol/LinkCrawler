using LinkCrawler.Models;

namespace LinkCrawler
{
    public interface ILinkCrawler
    {
        string BaseUrl { get; set; }
        void Start();
        void SendRequest(string crawlUrl, string referrerUrl = "");
        void ProcessResponse(ResponseModel responseModel);
        void FindAndCrawlForLinksInResponse(ResponseModel responseModel);
        void WriteOutputAndNotifySlack(ResponseModel responseModel);
    }
}