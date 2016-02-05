namespace LinkCrawler
{
    public static class Constants
    {
        public static class AppSettings
        {
            public const string BaseUrl = "BaseUrl";
        }

        public static class Response
        {
            public const string ContentTypeTextHtml = "text/html";
        }
        public static class Html
        {
            public const string Mailto = "mailto";
            public const string Href = "href";
            public const string LinkSearchPattern = "//a[@href]";
        }
    }
}
