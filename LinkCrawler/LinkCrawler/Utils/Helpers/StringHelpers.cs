using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace LinkCrawler.Utils.Helpers
{
    public static class StringHelpers
    {
        private static List<string> GetUrlsFromHtmlDocument(string markup, string searchPattern, string attribute)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(markup);
            var nodes = htmlDoc.DocumentNode.SelectNodes(searchPattern);

<<<<<<< Updated upstream
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
=======
            if (nodes == null || !nodes.Any())
                return new List<string>();

            return nodes.Select(x => x.GetAttributeValue(attribute, string.Empty).TrimEnd('/')).ToList();
>>>>>>> Stashed changes

        }

<<<<<<< Updated upstream
            return urls;
=======
        public static List<string> GetUrlListFromMarkup(string markup)
        {
            var linkUrls = GetUrlsFromHtmlDocument(markup, Constants.Html.LinkSearchPattern, Constants.Html.Href);
            var imgUrls = GetUrlsFromHtmlDocument(markup, Constants.Html.ImgSearchPattern, Constants.Html.Src);
            linkUrls.AddRange(imgUrls);
            return linkUrls;
>>>>>>> Stashed changes
        }
    }
}
