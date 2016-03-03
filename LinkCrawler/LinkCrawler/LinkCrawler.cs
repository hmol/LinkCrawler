using LinkCrawler.Models;
using LinkCrawler.Utils.Clients;
using LinkCrawler.Utils.Extensions;
using LinkCrawler.Utils.Helpers;
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
        public bool CheckImages;
        public RestClient RestClient;
        public RestRequest RestRequest;
        public IValidUrlParser ValidUrlParser;
        public static ISlackClient SlackClient;
        public bool OnlyReportBrokenLinksToOutput;
        public static List<string> VisitedUrlList;

        public LinkCrawler(ISlackClient slackClient, IValidUrlParser validUrlParser, ISettings settings)
        {
            SlackClient = slackClient;
            BaseUrl = settings.BaseUrl;
            RestClient = new RestClient();
            ValidUrlParser = validUrlParser;
            CheckImages = settings.CheckImages;
            VisitedUrlList = new List<string>();
            RestRequest = new RestRequest(Method.GET).SetHeader("Accept", "*/*");
            OnlyReportBrokenLinksToOutput = settings.OnlyReportBrokenLinksToOutput;
        }

        public void Start()
        {
            SendRequest(BaseUrl);
        }

        public void SendRequest(string crawlUrl, string referrerUrl = "")
        {
            var requestModel = new RequestModel(crawlUrl, referrerUrl, BaseUrl);
            RestClient.BaseUrl = new Uri(crawlUrl);
            RestClient.ExecuteAsync(RestRequest, response =>
            {
                if (response == null)
                    return;

                var responseModel = new ResponseModel(response, requestModel);
                ProcessResponse(responseModel);
            });
        }

        public void ProcessResponse(IResponseModel responseModel)
        {
            WriteOutputAndNotifySlack(responseModel);

            if (responseModel.ShouldCrawl)
                FindAndCrawlForLinksInResponse(responseModel);
        }

        public void FindAndCrawlForLinksInResponse(IResponseModel responseModel)
        {
            var linksFoundInMarkup = MarkupHelpers.GetValidUrlListFromMarkup(responseModel.Markup, ValidUrlParser, CheckImages);

            foreach (var url in linksFoundInMarkup)
            {
                if (VisitedUrlList.Contains(url))
                    continue;

                VisitedUrlList.Add(url);
                SendRequest(url, responseModel.RequestedUrl);
            }
        }

        public void WriteOutputAndNotifySlack(IResponseModel responseModel)
        {
            if (!responseModel.IsSucess)
            {
                ConsoleHelper.WriteError(responseModel.ToString());
                SlackClient.NotifySlack(responseModel);
            }
            else if (!OnlyReportBrokenLinksToOutput)
            {
                Console.WriteLine(responseModel.ToString());
            }
        }
    }
}