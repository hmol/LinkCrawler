using LinkCrawler.Models;
using LinkCrawler.Utils.Settings;
using RestSharp;

namespace LinkCrawler.Utils.Clients
{
    public class SlackClient : ISlackClient
    {
        public string WebHookUrl { get; set; }
        public string BotName { get; set; }
        public string BotIcon { get; set; }
        public string MessageFormat { get; set; }

        public bool HasWebHookUrl
        {
            get { return !string.IsNullOrEmpty(WebHookUrl); }
        }

        public SlackClient(ISettings settings)
        {
            WebHookUrl = settings.SlackWebHookUrl;
            BotName = settings.SlackWebHookBotName;
            BotIcon = settings.SlackWebHookBotIconEmoji;
            MessageFormat = settings.SlackWebHookBotMessageFormat;
        }

        public void NotifySlack(ResponseModel responseModel)
        {
            if (!HasWebHookUrl)
                return;

            var message = string.Format(MessageFormat, responseModel.RequestedUrl, responseModel.StatusCodeNumber, responseModel.ReferrerUrl);

            var client = new RestClient(WebHookUrl);
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(
                new
                {
                    text = message,
                    username = BotName,
                    icon_emoji = BotIcon,
                    mrkdwn = true
                });

            client.ExecuteAsync(request, null);
        }
    }
}
