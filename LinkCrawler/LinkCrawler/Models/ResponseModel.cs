using System;
using System.Net;
using LinkCrawler.Utils.Extensions;
using RestSharp;

namespace LinkCrawler.Models
{
    public class ResponseModel
    {
        public string Markup, RequestedUrl, ReferrerUrl;
        public HttpStatusCode StatusCode;
        public int StatusCodeNumber { get { return (int)StatusCode; } }
        public bool IsSucess, ShouldCrawl;

        public ResponseModel(IRestResponse restResponse, RequestModel requestModel)
        {
            ReferrerUrl = requestModel.ReferrerUrl;
            StatusCode = restResponse.StatusCode;
            RequestedUrl = requestModel.Url;
            IsSucess = (StatusCodeNumber > 99 && StatusCodeNumber < 300) || StatusCodeNumber >= 900;
            if (! IsSucess)
                return;
            Markup = restResponse.Content;
            ShouldCrawl = IsSucess && requestModel.IsInternalUrl && restResponse.IsHtmlDocument();
        }

        public override string ToString()
        {
            if(! IsSucess)
                return string.Format("{0}\t{1}\t{2}{3}\treferer: {4}", StatusCodeNumber, StatusCode, RequestedUrl, Environment.NewLine, ReferrerUrl);

            return string.Format("{0}   {1}   {2}", StatusCodeNumber, StatusCode, RequestedUrl);
        }
    }
}