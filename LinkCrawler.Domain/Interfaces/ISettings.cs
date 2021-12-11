
namespace LinkCrawler.Domain.Interfaces;

public interface ISettings
{
    string BaseUrl { get; set; }

    string ValidUrlRegex { get; }

    bool CheckImages { get; set; }

    bool OnlyReportBrokenLinksToOutput { get; }

    string SlackWebHookUrl { get; }

    string SlackWebHookBotName { get; }

    string SlackWebHookBotIconEmoji { get; }

    string SlackWebHookBotMessageFormat { get; }

    string CsvFilePath { get; }

    bool CsvOverwrite { get; }

    string CsvDelimiter { get; }

    bool IsSuccess(HttpStatusCode statusCode);

    IEnumerable<IOutput> Outputs { get; set; }

    bool PrintSummary { get; set; }
}
