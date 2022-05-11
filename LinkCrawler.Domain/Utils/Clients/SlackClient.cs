
namespace LinkCrawler.Domain.Utils.Clients;

public class SlackClient : ISlackClient
{

    public SlackClient(ISettings settings)
    {
        WebHookUrl = settings.SlackWebHookUrl;
        BotName = settings.SlackWebHookBotName;
        BotIcon = settings.SlackWebHookBotIconEmoji;
        MessageFormat = settings.SlackWebHookBotMessageFormat;
    }

    public async Task  NotifySlackAsync(IResponseModel responseModel)
    {
        if (!HasWebHookUrl)
            return;

        var message = string.Format(MessageFormat, responseModel.RequestedUrl, responseModel.StatusCodeNumber, responseModel.ReferrerUrl);

        var client = new RestClient(WebHookUrl);
        var request = new RestRequest(Method.Post.ToString()) { RequestFormat = DataFormat.Json };
        request.AddBody(
            new
            {
                text = message,
                username = BotName,
                icon_emoji = BotIcon,
                mrkdwn = true
            });

        await client.ExecuteAsync(request);
    }

    public string BotIcon { get; set; }
    public string BotName { get; set; }

    public bool HasWebHookUrl
    {
        get { return !string.IsNullOrEmpty(WebHookUrl); }
    }
    public string MessageFormat { get; set; }
    public string WebHookUrl { get; set; }
}
