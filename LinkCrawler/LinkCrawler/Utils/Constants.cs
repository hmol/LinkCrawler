namespace LinkCrawler.Utils
{
    public static class Constants
    {
        public static class AppSettings
        {
            public const string BaseUrl = "BaseUrl";
            public const string CheckImages = "CheckImages";
            public const string SlackWebHookUrl = "Slack.WebHook.Url";
            public const string SlackWebHookBotName = "Slack.WebHook.Bot.Name";
            public const string SlackWebHookBotIconEmoji = "Slack.WebHook.Bot.IconEmoji";
        }

        public static class Response
        {
            public const string ContentTypeTextHtml = "text/html";
        }

        public static class Html
        {
            public const string Mailto = "mailto:";
            public const string Tel = "tel:";
            public const string Href = "href";
            public const string Src = "src";
            public const string LinkSearchPattern = "//a[@href]";
            public const string ImgSearchPattern = "//img[@src]";
        }
    }
}
