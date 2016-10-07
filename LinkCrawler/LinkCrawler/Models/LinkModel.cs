namespace LinkCrawler.Models
{
    public class LinkModel
    {
        public string Url { get; set; }
        public string InnerHtml { get; }

        public LinkModel(string url, string innerHtml = "")
        {
            Url = url;
            InnerHtml = innerHtml;
        }
    }
}
