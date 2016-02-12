using LinkCrawler.Models;
using RestSharp;

namespace LinkCrawler.Utils.Clients
{
    public class SlackClient
    {
        public const string MessageFormat = "There is a link not working. " +
                                            "\nUrl: {0}. " +
                                            "\nStatuscode: {1}." +
                                            "\nThe link is placed on this page: {2}";

        public string WebHookUrl, BotName, BotIcon;

        public SlackClient()
        {
            WebHookUrl = Settings.Instance.SlackWebHookUrl;
            BotName = Settings.Instance.SlackWebHookBotName;
            BotIcon = Settings.Instance.SlackWebHookBotIconEmoji;
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
