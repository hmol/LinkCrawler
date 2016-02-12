using LinkCrawler.Models;
using RestSharp;

namespace LinkCrawler.Utils.Clients
{
    public class SlackClient
    {
        public string WebHookUrl, BotName, BotIcon, MessageFormat;

        public SlackClient()
        {
            WebHookUrl = Settings.Instance.SlackWebHookUrl;
            BotName = Settings.Instance.SlackWebHookBotName;
            BotIcon = Settings.Instance.SlackWebHookBotIconEmoji;
            MessageFormat = Settings.Instance.SlackWebHookBotMessageFormat;
        }

        public void NotifySlack(ResponseModel responseModel, string referrerUrl)
        {
            var message = string.Format(MessageFormat, responseModel.Url, responseModel.StatusCodeNumber, referrerUrl);

            var client = new RestClient(WebHookUrl);
            var request = new RestRequest(Method.POST) {RequestFormat = DataFormat.Json};
            request.AddBody(
                new
                {
                      text = message,
                      username = BotName,
                      icon_emoji = BotIcon
                });

            client.ExecuteAsync(request, null);
        }
    }
}
