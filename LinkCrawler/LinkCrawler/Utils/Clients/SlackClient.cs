using RestSharp;

namespace LinkCrawler.Utils.Clients
{
    public class SlackClient
    {
        private static SlackClient _instance;
        public static SlackClient Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                return _instance = new SlackClient();
            }
        }

        public const string MessageFormat = "There is a link not working. The links points to this url: {0}. " +
                                             "The link is placed on this page: {1}";

        public void NotifySlack(string url, string referrerUrl)
        {
            var message = string.Format(MessageFormat, url, referrerUrl);

            var client = new RestClient(Settings.Instance.SlackWebHookUrl);
            var request = new RestRequest(Method.POST) {RequestFormat = DataFormat.Json};
            request.AddBody(
                new { text = message,
                      username = Settings.Instance.SlackWebHookBotName,
                      icon_emoji = Settings.Instance.SlackWebHookBotIconEmoji
                });

            client.Execute(request);
        }
    }
}
