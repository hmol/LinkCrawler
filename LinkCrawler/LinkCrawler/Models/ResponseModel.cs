using LinkCrawler.Utils.Extensions;
using RestSharp;
using System;
using System.Net;

namespace LinkCrawler.Models
{
    public class ResponseModel : IResponseModel
    {
        public string Markup { get; }
        public string RequestedUrl { get; }
        public string ReferrerUrl { get; }

        public HttpStatusCode StatusCode { get; }
        public int StatusCodeNumber { get { return (int)StatusCode; } }
        public bool IsSucess { get; }
        public bool ShouldCrawl { get; }

        public ResponseModel(IRestResponse restResponse, RequestModel requestModel)
        {
            ReferrerUrl = requestModel.ReferrerUrl;
            StatusCode = restResponse.StatusCode;
            RequestedUrl = requestModel.Url;
            IsSucess = (StatusCodeNumber > 99 && StatusCodeNumber < 300) || StatusCodeNumber >= 900;
            if (!IsSucess)
                return;
            Markup = restResponse.Content;
            ShouldCrawl = IsSucess && requestModel.IsInternalUrl && restResponse.IsHtmlDocument();
        }

        public override string ToString()
        {
            if (!IsSucess)
                return string.Format("{0}\t{1}\t{2}{3}\tReferer:\t{4}", StatusCodeNumber, StatusCode, RequestedUrl, Environment.NewLine, ReferrerUrl);

            return string.Format("{0}\t{1}\t{2}", StatusCodeNumber, StatusCode, RequestedUrl);
        }
    }
}