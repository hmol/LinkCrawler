using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace LinkCrawler.Utils.Helpers
{
    public static class MarkupHelpers
    {
        private static List<string> GetUrlsFromHtmlDocument(string markup, string searchPattern, string attribute)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(markup);
            var nodes = htmlDoc.DocumentNode.SelectNodes(searchPattern);

            if (nodes == null || !nodes.Any())
                return new List<string>();

            return nodes.Select(x => x.GetAttributeValue(attribute, string.Empty).TrimEnd('/')).ToList();
        }

        public static List<string> GetUrlListFromMarkup(string markup, bool checkImageTags)
        {
            var linkUrls = GetUrlsFromHtmlDocument(markup, Constants.Html.LinkSearchPattern, Constants.Html.Href);
            if (checkImageTags)
            {
                var imgUrls = GetUrlsFromHtmlDocument(markup, Constants.Html.ImgSearchPattern, Constants.Html.Src);
                linkUrls.AddRange(imgUrls);
            }
            return linkUrls;
        }
    }
}
