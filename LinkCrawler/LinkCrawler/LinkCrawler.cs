using LinkCrawler.Models;
using LinkCrawler.Utils.Extensions;
using LinkCrawler.Utils.Helpers;
using LinkCrawler.Utils.Outputs;
using LinkCrawler.Utils.Parsers;
using LinkCrawler.Utils.Settings;
using RestSharp;
using System;
using System.Collections.Generic;

namespace LinkCrawler
{
    public class LinkCrawler
    {
        public string BaseUrl { get; set; }
        public bool CheckImages { get; set; }
        public RestRequest RestRequest { get; set; }
        public IEnumerable<IOutput> Outputs { get; set; }
        public IValidUrlParser ValidUrlParser { get; set; }
        public bool OnlyReportBrokenLinksToOutput { get; set; }
        public static List<string> VisitedUrlList { get; set; }
        private ISettings _settings;

        public LinkCrawler(IEnumerable<IOutput> outputs, IValidUrlParser validUrlParser, ISettings settings)
        {
            BaseUrl = settings.BaseUrl;
            Outputs = outputs;
            ValidUrlParser = validUrlParser;
            CheckImages = settings.CheckImages;
            VisitedUrlList = new List<string>();
            RestRequest = new RestRequest(Method.GET).SetHeader("Accept", "*/*");
            OnlyReportBrokenLinksToOutput = settings.OnlyReportBrokenLinksToOutput;
            _settings = settings;
        }

        public void Start()
        {
            SendRequest(new LinkModel(BaseUrl));
        }

        public void SendRequest(LinkModel crawlLink, string referrerUrl = "")
        {
            var requestModel = new RequestModel(crawlLink.Url, referrerUrl, BaseUrl);
            var restClient = new RestClient(new Uri(crawlLink.Url)) { FollowRedirects = false };

            restClient.ExecuteAsync(RestRequest, response =>
            {
                if (response == null)
                    return;

                var responseModel = new ResponseModel(response, requestModel, _settings);

                if (!responseModel.IsSuccess)
                    responseModel.InnerHtml = crawlLink.InnerHtml;

                ProcessResponse(responseModel);
            });
        }

        public void ProcessResponse(IResponseModel responseModel)
        {
            WriteOutput(responseModel);

            if (responseModel.ShouldCrawl)
                CrawlForLinksInResponse(responseModel);
        }

        public void CrawlForLinksInResponse(IResponseModel responseModel)
        {
            var linksFoundInMarkup = MarkupHelpers.GetValidUrlListFromMarkup(responseModel.Markup, ValidUrlParser, CheckImages);

            foreach (var link in linksFoundInMarkup)
            {
                if (VisitedUrlList.Contains(link.Url))
                    continue;

                VisitedUrlList.Add(link.Url);
                SendRequest(link, responseModel.RequestedUrl);
            }
        }

        public void WriteOutput(IResponseModel responseModel)
        {
            if (!responseModel.IsSuccess)
            {
                foreach (var output in Outputs)
                {
                    output.WriteError(responseModel);
                }
            }
            else if (!OnlyReportBrokenLinksToOutput)
            {
                foreach (var output in Outputs)
                {
                    output.WriteInfo(responseModel);
                }
            }
        }
    }
}