using LinkCrawler.Models;
using LinkCrawler.Utils.Extensions;
using LinkCrawler.Utils.Helpers;
using LinkCrawler.Utils.Outputs;
using LinkCrawler.Utils.Parsers;
using LinkCrawler.Utils.Settings;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
        public static List<LinkModel> UrlList;
        private ISettings _settings;
        private Stopwatch timer;

        public LinkCrawler(IEnumerable<IOutput> outputs, IValidUrlParser validUrlParser, ISettings settings)
        {
            BaseUrl = settings.BaseUrl;
            Outputs = outputs;
            ValidUrlParser = validUrlParser;
            CheckImages = settings.CheckImages;
            UrlList = new List<LinkModel>();
            RestRequest = new RestRequest(Method.GET).SetHeader("Accept", "*/*");
            OnlyReportBrokenLinksToOutput = settings.OnlyReportBrokenLinksToOutput;
            _settings = settings;
            this.timer = new Stopwatch();
        }

        public void Start()
        {
            this.timer.Start();
            UrlList.Add(new LinkModel(BaseUrl));
            SendRequest(BaseUrl);
        }

        public void SendRequest(string crawlUrl, string referrerUrl = "")
        {
            var requestModel = new RequestModel(crawlUrl, referrerUrl, BaseUrl);
            var restClient = new RestClient(new Uri(crawlUrl)) { FollowRedirects = false };

            restClient.ExecuteAsync(RestRequest, response =>
            {
                if (response == null)
                    return;

                var responseModel = new ResponseModel(response, requestModel, _settings);
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

            foreach (var url in linksFoundInMarkup)
            {
                if (UrlList.Where(l => l.Address == url).Count() > 0)
                    continue;

                UrlList.Add(new LinkModel(url));
                SendRequest(url, responseModel.RequestedUrl);
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

            CheckIfFinal(responseModel);
        }

        private void CheckIfFinal(IResponseModel responseModel)
        {
            // First set the status code for the completed link (this will set "CheckingFinished" to true)
            foreach (LinkModel lm in UrlList.Where(l => l.Address == responseModel.RequestedUrl))
            {
                lm.StatusCode = responseModel.StatusCodeNumber;
            }

            // Then check to see whether there are any pending links left to check
            if(UrlList.Where(l => l.CheckingFinished == false).Count() == 0)
            {
                FinaliseSession();
            }
        }

        private void FinaliseSession()
        {
            this.timer.Stop();
            if (this._settings.PrintSummary)
            {
                string message = @"
Processing completed in " + this.timer.ElapsedMilliseconds.ToString() + "ms";
                foreach (var output in Outputs)
                {
                    output.WriteInfo(message);
                }
            }
        }
    }
}