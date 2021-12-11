
namespace LinkCrawler.Domain.Utils.Settings;

public class MockSettings : ISettings
{
    public MockSettings()
    {
        BaseUrl = "http://texecon.com";
        CheckImages = true;
        PrintSummary = true;
    }

    public MockSettings(bool includeWebHookUrl)
    {
        this.IncludeWebHookUrl = includeWebHookUrl;
    }

    private bool IncludeWebHookUrl { get; set; }

    public bool IsSuccess(HttpStatusCode statusCode)
    {
        return statusCode.IsSuccess("1xx,2xx,3xx");
    }

    public string BaseUrl { get; set; }

    public bool CheckImages { get; set; }

    public string CsvDelimiter => ";";

    public string CsvFilePath => @"D:\Test\LinkCrawlerOutput.csv";

    public bool CsvOverwrite => true;

    public bool OnlyReportBrokenLinksToOutput => false;
    public IEnumerable<IOutput> Outputs { get; set; }
    public bool PrintSummary { get; set; }

    public string SlackWebHookBotIconEmoji => ":homer:";

    public string SlackWebHookBotMessageFormat => "*Doh! There is a link not working* Url: {0} Status code: {1} The link is placed on this page: {2}";

    public string SlackWebHookBotName => "Homer Bot";
    public string SlackWebHookUrl
    {
        get { return IncludeWebHookUrl ? @"https://hooks.slack.com/services/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" : string.Empty; }
    }

    public string ValidUrlRegex => @"(^http[s]?:\/{2})|(^www)|(^\/{1,2})";
}
