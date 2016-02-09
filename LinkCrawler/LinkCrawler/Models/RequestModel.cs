using LinkCrawler.Utils;
using RestSharp;

namespace LinkCrawler.Models
{
    public class RequestModel
    {
        public string Url;
        public bool IsInternalUrl { get; set; }
        public RequestModel(string url)
        {
            Url = url;
            IsInternalUrl = url.StartsWith(Settings.Instance.BaseUrl);
        }

        public ResponseModel SendRequest()
        {
            var restClient = new RestClient(Url);
            var restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Accept", "*/*");
            var restResponse = restClient.Execute(restRequest);

            var responseModel = new ResponseModel(restResponse, Url);
            return responseModel;
        }
    }
}
