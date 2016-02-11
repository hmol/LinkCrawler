using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LinkCrawler.Models;
using LinkCrawler.Utils;
using LinkCrawler.Utils.Clients;
using LinkCrawler.Utils.Helpers;
using RestSharp;

namespace LinkCrawler
{
    public class LinkCrawler
    {
        public static List<string> VisitedUrlList;
        public static SlackClient SlackClient;


        public LinkCrawler()
        {
            VisitedUrlList = new List<string>();
            SlackClient = new SlackClient();
        }

        public void CrawlLinks()
        {
            CrawlLink(Settings.Instance.BaseUrl);
        }

        public void CrawlLink(string crawlUrl, string referrerUrl = "")
        {
            SendRequest(crawlUrl);
        }

        public void SendRequest(string url)
        {
            var restClient = new RestClient(url);
            var restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Accept", "*/*");
            restClient.ExecuteAsync(restRequest, SendRequestCallback);
        }

        public void CrawlLInksInResponse(RequestModel requestModel, ResponseModel responseModel)
        {
           
            if (!responseModel.IsSucess || !requestModel.IsInternalUrl || !responseModel.IsHtmlDocument)
                return;

            var linksFoundInMarkup = GetListOfUrls(responseModel.Markup);

            foreach (var url in linksFoundInMarkup)
            {
                if (VisitedUrlList.Contains(url))
                    continue;

                VisitedUrlList.Add(url);
                CrawlLink(url, requestModel.Url);
            }
        }

        private void SendRequestCallback(IRestResponse restResponse, RestRequestAsyncHandle restRequestAsyncHandle)
        {
            var vv =  new ResponseModel(restResponse, restResponse.ResponseUri.ToString());
            var vvv = new RequestModel(restResponse.ResponseUri.ToString());
            Console.WriteLine(vv);
            CrawlLInksInResponse(vvv, vv);
        }

        public List<string> GetListOfUrls(string markup)
        {
            var urlList = MarkupHelpers.GetUrlListFromMarkup(markup, Settings.Instance.CheckImages);
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
                    var newUrl = string.Concat(Settings.Instance.BaseUrl, url);
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
                //SlackClient.NotifySlack(responseModel, referrerUrl);
            }
        }

        private void WriteOutputAndNotifySlack2(ResponseModel responseModel, string referrerUrl)
        {
            Console.Out.WriteLineAsync(responseModel.ToString());
            if (!responseModel.IsSucess)
            {
                Console.Out.WriteLineAsync("Reffered in: " + referrerUrl);
                SlackClient.NotifySlack(responseModel, referrerUrl);
            }
        }
    }
}