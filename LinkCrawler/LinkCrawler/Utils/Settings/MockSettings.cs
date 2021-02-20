using LinkCrawler.Utils.Extensions;
using System.Net;

namespace LinkCrawler.Utils.Settings {
    public class MockSettings : ISettings {

        public string BaseUrl => "https://github.com";

        public bool CheckImages => true;

        public string CsvDelimiter => ";";

        public string CsvFilePath => @"C:\tmp\output.csv";

        public bool CsvOverwrite => true;

        public bool OnlyReportBrokenLinksToOutput => false;

        public string SlackWebHookBotIconEmoji => ":homer:";

        public string SlackWebHookBotMessageFormat => "*Doh! There is a link not working* Url: {0} Statuscode: {1} The link is placed on this page: {2}";

        public string SlackWebHookBotName => "Homer Bot";
        public bool PrintSummary => false;
        private bool IncludeWebHookUrl { get; set; }
        public string SlackWebHookUrl
        {
            get
            {
                return IncludeWebHookUrl ? @"https://hooks.slack.com/services/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" : "";
            }
        }

        public string ValidUrlRegex => @"(^http[s]?:\/{2})|(^www)|(^\/{1,2})";

        public bool IsSuccess(HttpStatusCode statusCode) {
            return statusCode.IsSuccess("1xx,2xx,3xx");
        }

        public bool FollowRedirects => false;

        public int[] FollowCodes => new int[] { 301, 302 };

        public MockSettings(bool includeWebHookUrl) {
            this.IncludeWebHookUrl = includeWebHookUrl;
        }
    }
}
