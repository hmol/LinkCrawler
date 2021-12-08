using RestSharp;

namespace LinkCrawler.Models
{
    public class RequestModel
    {
        public string Url;
        public string ReferrerUrl;
        public bool IsInternalUrl { get; set; }
        public RestClient Client;

        public RequestModel(string url, string referrerUrl, string baseUrl)
        {
            Url = url;
            IsInternalUrl = url.StartsWith(baseUrl);
            ReferrerUrl = referrerUrl;
            Client = new RestClient(Url);
        }
    }
}
