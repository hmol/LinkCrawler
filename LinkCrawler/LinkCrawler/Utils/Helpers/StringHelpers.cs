using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace LinkCrawler.Utils.Helpers
{
    public static class StringHelpers
    {
        private static HtmlNodeCollection GetHtmlNodes(string markup)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(markup);
            return htmlDoc.DocumentNode.SelectNodes(Constants.Html.LinkSearchPattern);
        }

        public static List<string> GetUrlListFromMarkup(string markup)
        {
            if (string.IsNullOrEmpty(markup))
                return new List<string>();

            var nodes = GetHtmlNodes(markup);

            if (nodes == null || !nodes.Any())
                return new List<string>();

            return nodes.Select(x => x.GetAttributeValue(Constants.Html.Href, string.Empty).TrimEnd('/')).ToList();
        }
    }
}
