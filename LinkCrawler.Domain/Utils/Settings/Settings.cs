
namespace LinkCrawler.Domain.Utils.Settings;

public class Settings : ISettings
{
    public Settings()
    {
        BaseUrl = GetConfigString(Constants.AppSettings.BaseUrl);
        CheckImages = GetConfigBool(Constants.AppSettings.CheckImages);
        PrintSummary = GetConfigBool(Constants.AppSettings.PrintSummary);
        if (Outputs is null) Outputs = new List<IOutput>();
    }

    private static string GetConfigString(string configValue)
    {
        if (ConfigurationManager.AppSettings[configValue] == null) return string.Empty;
        return ConfigurationManager.AppSettings[configValue]!.Trim('/');
    }
    private static bool GetConfigBool(string configValue)
    {
        if (ConfigurationManager.AppSettings[configValue] == null) return false;
        return ConfigurationManager.AppSettings[configValue]!.ToBool();
    }

    public bool IsSuccess(HttpStatusCode statusCode)
    {
        var configuredCodes = ConfigurationManager.AppSettings[Constants.AppSettings.SuccessHttpStatusCodes] ?? string.Empty;
        return statusCode.IsSuccess(configuredCodes);
    }
    public IEnumerable<IOutput> Outputs { get; set; }

    public string BaseUrl { get; set; }

    public bool CheckImages { get; set; }

    public string CsvDelimiter => GetConfigString(Constants.AppSettings.CsvDelimiter);

    public string CsvFilePath => GetConfigString(Constants.AppSettings.CsvFilePath);

    public bool CsvOverwrite => GetConfigBool(Constants.AppSettings.CsvOverwrite);

    public bool OnlyReportBrokenLinksToOutput => GetConfigBool(Constants.AppSettings.OnlyReportBrokenLinksToOutput);

    public bool PrintSummary { get; set; }

    public string SlackWebHookBotIconEmoji => GetConfigString(Constants.AppSettings.SlackWebHookBotIconEmoji);

    public string SlackWebHookBotMessageFormat => GetConfigString(Constants.AppSettings.SlackWebHookBotMessageFormat);

    public string SlackWebHookBotName => GetConfigString(Constants.AppSettings.SlackWebHookBotName);

    public string SlackWebHookUrl => GetConfigString(Constants.AppSettings.SlackWebHookUrl);

    public string ValidUrlRegex => GetConfigString(Constants.AppSettings.ValidUrlRegex);
}
