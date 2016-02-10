using System.Net;
using LinkCrawler.Utils;
using RestSharp;

namespace LinkCrawler.Models
{
    public class ResponseModel
    {
        public string Markup { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public int StatusCodeNumber { get { return (int)StatusCode; } }
        public bool IsSucess;
        public bool IsHtmlDocument { get; set; }
        public string Url { get; set; }

        public ResponseModel(IRestResponse restResponse, string url)
        {
            StatusCode = restResponse.StatusCode;
            IsSucess = StatusCodeNumber > 99 && StatusCodeNumber < 300;
            Url = url;
            if(StatusCode != HttpStatusCode.OK)
                return;
            Markup = restResponse.Content;
            IsHtmlDocument = restResponse.ContentType.StartsWith(Constants.Response.ContentTypeTextHtml);
        }

        public override string ToString()
        {
            return string.Format("{0}   {1}   {2}", StatusCodeNumber, StatusCode, Url);
        }
    }
}