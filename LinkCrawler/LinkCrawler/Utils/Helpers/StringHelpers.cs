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

        private static HtmlNodeCollection GetImgNodes(string markup)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(markup);
            return htmlDoc.DocumentNode.SelectNodes(Constants.Html.ImgSearchPattern);
        }

        public static List<string> GetUrlListFromMarkup(string markup, bool findImagTags)
        {
            if (string.IsNullOrEmpty(markup))
                return new List<string>();

            var nodes = GetHtmlNodes(markup);
            var urls = nodes.Select(x => x.GetAttributeValue(Constants.Html.Href, string.Empty).TrimEnd('/')).ToList();

            if (findImagTags)
            {
                var imgNodes = GetImgNodes(markup);
                urls.AddRange(imgNodes.Select(x => x.GetAttributeValue(Constants.Html.Src, string.Empty).TrimEnd('/')).ToList());
            }

            if (nodes == null || !nodes.Any())
                return new List<string>();

            return urls;
        }
    }
}
