using System.Net;
using RestSharp;

namespace LinkCrawler.Models
{
    public class ResponseModel
    {
        public string Markup { get; set; }
        public System.Net.HttpStatusCode StatusCode { get; set; }
        public int StatusCodeNumber { get { return (int)StatusCode; } }
        public bool IsSucess { get { return StatusCode == HttpStatusCode.OK; } }
        public bool IsHtmlDocument { get; set; }
        public string Url { get; set; }

        public ResponseModel(IRestResponse restResponse, string url)
        {
            StatusCode = restResponse.StatusCode;
            Url = url;
            if(StatusCode != HttpStatusCode.OK)
                return;
            Markup = restResponse.Content;
            IsHtmlDocument = restResponse.ContentType.StartsWith(Constants.Html.HtmlContentType);
        }

        public override string ToString()
        {
            return string.Format("{0}   {1}   {2}", StatusCodeNumber, StatusCode, Url);
        }
    }
}
