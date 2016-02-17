using System;
using System.Collections.Generic;
using LinkCrawler.Models;
using LinkCrawler.Utils;
using LinkCrawler.Utils.Clients;
using LinkCrawler.Utils.Extensions;
using LinkCrawler.Utils.Helpers;
using RestSharp;

namespace LinkCrawler
{
    public class LinkCrawler
    {
        public static List<string> VisitedUrlList;
        public static SlackClient SlackClient;
        public string BaseUrl;
        public bool CheckImages, OnlyReportBrokenLinksToOutput;
        public RestRequest RestRequest;

        public LinkCrawler()
        {
            VisitedUrlList = new List<string>();
            SlackClient = new SlackClient();
            BaseUrl = Settings.Instance.BaseUrl;
            CheckImages = Settings.Instance.CheckImages;
            OnlyReportBrokenLinksToOutput = Settings.Instance.OnlyReportBrokenLinksToOutput;
            RestRequest = new RestRequest(Method.GET).SetHeader("Accept", "*/*");
        }

        public void Start()
        {
            SendRequest(BaseUrl);
        }

        public void SendRequest(string crawlUrl, string referrerUrl = "")
        {
            var requestModel = new RequestModel(crawlUrl, referrerUrl);
            requestModel.Client.ExecuteAsync(RestRequest, response =>
            {
                if(response == null)
                    return;

                var responseModel = new ResponseModel(response, requestModel);
                ProsessResponse(responseModel);
            });
        }

        private void ProsessResponse(ResponseModel responseModel)
        {
            WriteOutputAndNotifySlack(responseModel);

            if (responseModel.ShouldCrawl)
                FindAndCrawlForLinksInResponse(responseModel);
        }

        public void FindAndCrawlForLinksInResponse(ResponseModel responseModel)
        {
            var linksFoundInMarkup = MarkupHelpers.GetValidUrlListFromMarkup(responseModel.Markup, CheckImages, BaseUrl);

            foreach (var url in linksFoundInMarkup)
            {
                if (VisitedUrlList.Contains(url))
                    continue;

                VisitedUrlList.Add(url);
                SendRequest(url, responseModel.RequestedUrl);
            }
        }

        private void WriteOutputAndNotifySlack(ResponseModel responseModel)
        {
            if (!responseModel.IsSucess)
            {
                ConsoleHelper.WriteError(responseModel.ToString());
                SlackClient.NotifySlack(responseModel);
            }
            else if(!OnlyReportBrokenLinksToOutput)
            {
                Console.WriteLine(responseModel.ToString());
            }
        }
    }
}