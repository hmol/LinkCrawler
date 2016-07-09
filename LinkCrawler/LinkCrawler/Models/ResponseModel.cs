﻿using LinkCrawler.Utils.Extensions;
using RestSharp;
using System;
using System.Net;
using LinkCrawler.Utils.Settings;

namespace LinkCrawler.Models
{
    public class ResponseModel : IResponseModel
    {
        public string Markup { get; }
        public string RequestedUrl { get; }
        public string ReferrerUrl { get; }

        public HttpStatusCode StatusCode { get; }
        public int StatusCodeNumber { get { return (int)StatusCode; } }
        public bool IsSuccess { get; }
        public bool ShouldCrawl { get; }
        public TimeSpan ElapsedTimeSpan { get; set; }

        public ResponseModel(IRestResponse restResponse, RequestModel requestModel, ISettings settings)
        {
            ReferrerUrl = requestModel.ReferrerUrl;
            StatusCode = restResponse.StatusCode;
            RequestedUrl = requestModel.Url;
            IsSuccess = settings.IsSuccess(StatusCode);
            if (!IsSuccess)
                return;
            Markup = restResponse.Content;
            ShouldCrawl = IsSuccess && requestModel.IsInternalUrl && restResponse.IsHtmlDocument();
        }

        public override string ToString()
        {
            if (!IsSuccess)
                return string.Format("{0}\t{1}\t{2}{3}\tReferer:\t{4}", StatusCodeNumber, StatusCode, RequestedUrl, Environment.NewLine, ReferrerUrl);

            return string.Format("{0}\t{1}\t{2}\t{3}", StatusCodeNumber, StatusCode, RequestedUrl, ElapsedTimeSpan.ToString());
        }
    }
}