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
            if (Url.StartsWith("http://www.norrøna.no"))
            {
                
            }
            var restClient = new RestClient(Url);
            var restRequest = new RestRequest(Method.GET);
            var restResponse = restClient.Execute(restRequest);

            var responseModel = new ResponseModel(restResponse, Url);
            return responseModel;

        }
    }
}
