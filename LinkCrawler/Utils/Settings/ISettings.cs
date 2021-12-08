using System.Net;

namespace LinkCrawler.Utils.Settings
{
    public interface ISettings
    {
        string BaseUrl { get; }

        string ValidUrlRegex { get; }

        bool CheckImages { get; }

        bool OnlyReportBrokenLinksToOutput { get; }

        string SlackWebHookUrl { get; }

        string SlackWebHookBotName { get; }

        string SlackWebHookBotIconEmoji { get; }

        string SlackWebHookBotMessageFormat { get; }

        string CsvFilePath { get; }

        bool CsvOverwrite { get; }

        string CsvDelimiter { get; }

        bool IsSuccess(HttpStatusCode statusCode);

        bool PrintSummary { get; }
    }
}
