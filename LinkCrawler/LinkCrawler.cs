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

        private void CheckIfFinal(IResponseModel responseModel)
        {
            lock (UrlList)
            {

                // First set the status code for the completed link (this will set "CheckingFinished" to true)
                foreach (LinkModel lm in UrlList.Where(l => l.Address == responseModel.RequestedUrl))
                {
                    lm.StatusCode = responseModel.StatusCodeNumber;
                }

                // Then check to see whether there are any pending links left to check
                if ((UrlList.Count > 1) && (UrlList.Where(l => l.CheckingFinished == false).Count() == 0))
                {
                    FinaliseSession();
                }
            }
        }

        private void FinaliseSession()
        {
            this.timer.Stop();
            if (this._settings.PrintSummary)
            {
                List<string> messages = new List<string>();
                messages.Add(string.Empty); // add blank line to differentiate summary from main output

                messages.Add("Processing complete. Checked " + UrlList.Count() + " links in " + this.timer.ElapsedMilliseconds.ToString() + "ms");

                messages.Add(string.Empty);
                messages.Add(" Status | # Links");
                messages.Add(" -------+--------");

                IEnumerable<IGrouping<int, string>> StatusSummary = UrlList.GroupBy(link => link.StatusCode, link => link.Address);
                foreach (IGrouping<int, string> statusGroup in StatusSummary)
                {
                    messages.Add(String.Format("   {0}  | {1,5}", statusGroup.Key, statusGroup.Count()));
                }

                foreach (var output in Outputs)
                {
                    output.WriteInfo(messages.ToArray());
                }
            }
        }

        public void CrawlForLinksInResponse(IResponseModel responseModel)
        {
            var linksFoundInMarkup = MarkupHelpers.GetValidUrlListFromMarkup(responseModel.Markup, ValidUrlParser, CheckImages);

            foreach (var url in linksFoundInMarkup)
            {
                lock (UrlList)
                {
                    if (UrlList.Where(l => l.Address == url).Count() > 0)
                        continue;

                    UrlList.Add(new LinkModel(url));
                }
                SendRequest(url, responseModel.RequestedUrl);
            }
        }

        public void ProcessResponse(IResponseModel responseModel)
        {
            WriteOutput(responseModel);

            if (responseModel.ShouldCrawl)
                CrawlForLinksInResponse(responseModel);
        }

        public void SendRequest(string crawlUrl, string referrerUrl = "")
        {
            var requestModel = new RequestModel(crawlUrl, referrerUrl, BaseUrl);
            var restClient = new RestClient(new Uri(crawlUrl)) { FollowRedirects = true };

            restClient.ExecuteAsync(RestRequest, response =>
            {
                if (response == null)
                    return;

                var responseModel = new ResponseModel(response, requestModel, _settings);
                ProcessResponse(responseModel);
            });
        }

        public void Start()
        {
            this.timer.Start();
            UrlList.Add(new LinkModel(BaseUrl));
            SendRequest(BaseUrl);
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

        public string BaseUrl { get; set; }
        public bool CheckImages { get; set; }
        public bool OnlyReportBrokenLinksToOutput { get; set; }
        public IEnumerable<IOutput> Outputs { get; set; }
        public RestRequest RestRequest { get; set; }
        public IValidUrlParser ValidUrlParser { get; set; }
    }
}