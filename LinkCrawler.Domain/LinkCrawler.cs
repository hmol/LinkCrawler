namespace LinkCrawler.Domain
{
    public class LinkCrawler : ICrawler
    {
        private readonly ISettings _settings;
        private readonly Stopwatch timer;
        private readonly List<LinkModel> UrlList = new();
        private readonly RestRequest RestRequest;
        private readonly IValidUrlParser ValidUrlParser;

        public LinkCrawler()
        {
            _settings = new Settings();
            ValidUrlParser = new ValidUrlParser(_settings);
            RestRequest = new RestRequest(Method.Get.ToString()).SetHeader("Accept", "*/*");
            this.timer = new Stopwatch();

        }
        public LinkCrawler(ISettings settings)
        {
            _settings = settings;
            ValidUrlParser = new ValidUrlParser(settings);
            RestRequest = new RestRequest(Method.Get.ToString()).SetHeader("Accept", "*/*");
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
                if ((UrlList.Count > 1) && (!UrlList.Where(l => l.CheckingFinished == false).Any()))
                {
                    FinaliseSession();
                }
            }
        }

        private void CrawlForLinksInResponse(IResponseModel responseModel)
        {
            var linksFoundInMarkup = MarkupHelpers.GetValidUrlListFromMarkup(responseModel.Markup, ValidUrlParser, _settings.CheckImages);
            var StillToProcess = new List<string>();

            foreach (var url in linksFoundInMarkup.Distinct().ToArray())
            {
                lock (UrlList)
                {
                    var curList = UrlList.Select(s => s.Address).Distinct().ToArray();
                    if (curList.Contains(url))
                    {
                        continue;
                    }
                    else
                    {
                        StillToProcess.Add(url);
                        UrlList.Add(new LinkModel(url, responseModel.RequestedUrl));
                    }
                }
            }
            foreach (var url in StillToProcess)
            {
                SendRequest(url, responseModel.RequestedUrl);
            }


        }

        private void FinaliseSession()
        {
            this.timer.Stop();
            if (this._settings.PrintSummary)
            {
                List<string> messages = new();
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
                foreach (var output in _settings.Outputs)
                {
                    output.WriteInfo(messages.ToArray());
                }
                foreach (var msg in messages)
                    Console.WriteLine(msg);
            }
            this.timer.Start();
        }

        private void ProcessResponse(IResponseModel responseModel)
        {
            WriteOutput(responseModel);
            if (responseModel.ShouldCrawl)
                CrawlForLinksInResponse(responseModel);
        }

        private async Task SendRequest(string crawlUrl, string referrerUrl = "")
        {
            var requestModel = new RequestModel(crawlUrl, referrerUrl, _settings.BaseUrl);
            var restClient = new RestClient(new Uri(crawlUrl));
            var response = await restClient.ExecuteAsync(RestRequest);
            Console.WriteLine($"Crawling URL:{crawlUrl}");
            if (response == null) return;
            var responseModel = new ResponseModel(response, requestModel, _settings);
            ProcessResponse(responseModel);
        }

        private void WriteOutput(IResponseModel responseModel)
        {
            if (!responseModel.IsSuccess)
            {
                foreach (var output in _settings.Outputs)
                {
                    output.WriteErrorAsync(responseModel);
                }
            }
            else if (!_settings.OnlyReportBrokenLinksToOutput)
            {
                foreach (var output in _settings.Outputs)
                {
                    output.WriteInfo(responseModel);
                }
            }
            CheckIfFinal(responseModel);
        }

        public void Start()
        {
            this.timer.Start();
            UrlList.Add(new LinkModel(_settings.BaseUrl));
            SendRequest(_settings.BaseUrl);
        }
    }
}