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
        public bool CheckImages;
        public RestRequest RestRequest;

        public LinkCrawler()
        {
            VisitedUrlList = new List<string>();
            SlackClient = new SlackClient();
            BaseUrl = Settings.Instance.BaseUrl;
            CheckImages = Settings.Instance.CheckImages;
            RestRequest = new RestRequest(Method.GET).SetHeader("Accept", "*/*");
        }

        public void Start()
        {
            CrawlLink(BaseUrl);
        }

        public void CrawlLink(string crawlUrl, string referrerUrl = "")
        {
            var requestModel = new RequestModel(crawlUrl, referrerUrl);
            SendRequest(requestModel);
        }

        public void SendRequest(RequestModel requestModel)
        {
            requestModel.Client.ExecuteAsync(RestRequest, response =>
            {
                ProsessResponse(response, requestModel);
            });
        }

        private void ProsessResponse(IRestResponse restResponse, RequestModel requestModel)
        {
            if(restResponse == null)
                return;

            var responseModel =  new ResponseModel(restResponse, requestModel);
            WriteOutputAndNotifySlack(responseModel, requestModel.ReferrerUrl);

            if (responseModel.ShouldCrawl)
                FindAndCrawlLinks(responseModel, requestModel);
        }

        public void FindAndCrawlLinks(ResponseModel responseModel, RequestModel requestModel)
        {
            var linksFoundInMarkup = GetListOfUrlsFromMarkup(responseModel.Markup);

            foreach (var url in linksFoundInMarkup)
            {
                if (VisitedUrlList.Contains(url))
                    continue;

                VisitedUrlList.Add(url);
                CrawlLink(url, requestModel.Url);
            }
        }

        public List<string> GetListOfUrlsFromMarkup(string markup)
        {
            var urlList = MarkupHelpers.GetUrlListFromMarkup(markup, CheckImages);
            var correctUrlList = new List<string>();

            foreach (var url in urlList)
            {
                Uri parsedUri;
                if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out parsedUri)
                    || url.StartsWith(Constants.Html.Mailto)
                    || url.StartsWith(Constants.Html.Tel))
                    continue;

                if(!parsedUri.IsAbsoluteUri)
                {
                    var newUrl = string.Concat(BaseUrl, url);
                    correctUrlList.Add(newUrl);
                }
                else
                {
                    correctUrlList.Add(url);
                }
            }
            return correctUrlList;
        }

        private void WriteOutputAndNotifySlack(ResponseModel responseModel, string referrerUrl)
        {
            Console.WriteLine(responseModel.ToString());

            if (!responseModel.IsSucess)
            {
                Console.WriteLine("Reffered in: " + referrerUrl);
                SlackClient.NotifySlack(responseModel, referrerUrl);
            }
        }
    }
}