using HtmlAgilityPack;
using LinkCrawler.Utils.Parsers;
using LinkCrawler.Utils.Settings;
using System.Collections.Generic;
using System.Linq;
using LinkCrawler.Models;

namespace LinkCrawler.Utils.Helpers
{
    public static class MarkupHelpers
    {
        private static List<LinkModel> GetAllUrlsFromHtmlDocument(string markup, string searchPattern, string attribute)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(markup);
            var nodes = htmlDoc.DocumentNode.SelectNodes(searchPattern);

            if (nodes == null || !nodes.Any())
                return new List<LinkModel>();

            return nodes.Select(x => new LinkModel(x.GetAttributeValue(attribute, string.Empty).TrimEnd('/'), x.InnerHtml)).ToList();
        }

        public static List<LinkModel> GetAllUrlsFromMarkup(string markup, bool checkImageTags)
        {
            var links = GetAllUrlsFromHtmlDocument(markup, Constants.Html.LinkSearchPattern, Constants.Html.Href);
            if (checkImageTags)
            {
                var imgUrls = GetAllUrlsFromHtmlDocument(markup, Constants.Html.ImgSearchPattern, Constants.Html.Src);
                links.AddRange(imgUrls);
            }
            return links;
        }

        /// <summary>
        /// Get's a list of all urls in markup and tires to fix the urls that Restsharp will have a problem with 
        /// (i.e relative urls, urls with no sceme, mailto links..etc)
        /// </summary>
        /// <returns>List of urls that will work with restsharp for sending http get</returns>
        public static List<LinkModel> GetValidUrlListFromMarkup(string markup, IValidUrlParser parser, bool checkImages)
        {
            var linkList = GetAllUrlsFromMarkup(markup, checkImages);
            var validLinkList = new List<LinkModel>();

            foreach (var link in linkList)
            {
                string validLink;
                if (parser.Parse(link.Url, out validLink))
                {
                    link.Url = validLink;
                    validLinkList.Add(link);
                }
            }
            return validLinkList;
        }
    }
}
