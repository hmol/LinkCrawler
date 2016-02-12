using System.Net;
using LinkCrawler.Utils.Extensions;
using RestSharp;

namespace LinkCrawler.Models
{
    public class ResponseModel
    {
        public string Markup { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public int StatusCodeNumber { get { return (int)StatusCode; } }
        public bool IsSucess;

        public bool ShouldCrawl { get; set; }
        public string Url { get; set; }
        public RequestModel RequestModel { get; set; }

        public ResponseModel(IRestResponse restResponse, RequestModel requestModel)
        {
            StatusCode = restResponse.StatusCode;
            IsSucess = StatusCodeNumber > 99 && StatusCodeNumber < 300;
            Url = requestModel.Url;
            if(! IsSucess)
                return;
            Markup = restResponse.Content;
            ShouldCrawl = IsSucess && requestModel.IsInternalUrl && restResponse.IsHtmlDocument();
        }

        public override string ToString()
        {
            return string.Format("{0}   {1}   {2}", StatusCodeNumber, StatusCode, Url);
        }
    }
}