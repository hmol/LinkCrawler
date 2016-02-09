using System;
using System.Collections.Generic;
using LinkCrawler.Models;
using LinkCrawler.Utils.Helpers;

namespace LinkCrawler
{
    public class LinkCrawler
    {
        public static List<string> VisitedUrlList;

        public LinkCrawler()
        {
            VisitedUrlList = new List<string>();
        }

        public void CrawlLinks()
        {
            CrawlLink(Settings.Instance.BaseUrl);
        }

        private void CrawlLink(string crawlUrl, string referrerUrl = "")
        {
            var requestModel = new RequestModel(crawlUrl);
            var responseModel = requestModel.SendRequest();

            WriteOutput(responseModel, referrerUrl);

            if(!responseModel.IsSucess || !requestModel.IsInternalUrl || !responseModel.IsHtmlDocument)
                return;

            var linksFoundInMarkup = GetListOfUrls(responseModel.Markup);
            
            foreach (var url in linksFoundInMarkup)
            {
                if (VisitedUrlList.Contains(url))
                    continue;

                VisitedUrlList.Add(url);
                CrawlLink(url, crawlUrl);
            }
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

        private void WriteOutput(ResponseModel responseModel, string referrerUrl)
        {
            Console.WriteLine(responseModel.ToString());

            if (!responseModel.IsSucess)
            {
                Console.WriteLine("Reffered in: " + referrerUrl);
                SendToSlack(responseModel,referrerUrl);
            }
        }

        private void SendToSlack(ResponseModel responseModel, string referrerUrl)
        {
            //var errorUrlText = "There is a link not working. The links points to this url: <" + responseItem.Url + "|" + responseItem.Url + ">. " +
            //                       "The link is placed on this page: <" + referrerUrl + "|" + referrerUrl + ">";

            //var client = new RestClient("https://hooks.slack.com/");
            //var request = new RestRequest("/services/T024FQG21/B0LAVJT4H/lkk9qCg4pM2dC8yK8wwXFkLH", Method.POST);
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(new { text = errorUrlText });

            //client.Execute(request);
        }

    }
}