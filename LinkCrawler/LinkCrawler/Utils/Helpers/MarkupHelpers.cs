using HtmlAgilityPack;
using LinkCrawler.Utils.Parsers;
using LinkCrawler.Utils.Settings;
using System.Collections.Generic;
using System.Linq;

namespace LinkCrawler.Utils.Helpers
{
    public static class MarkupHelpers
    {
        private static List<string> GetAllUrlsFromHtmlDocument(string markup, string searchPattern, string attribute)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(markup);
            var nodes = htmlDoc.DocumentNode.SelectNodes(searchPattern);

            if (nodes == null || !nodes.Any())
                return new List<string>();

            return nodes.Select(x => x.GetAttributeValue(attribute, string.Empty).TrimEnd('/')).ToList();
        }

        public static List<string> GetAllUrlsFromMarkup(string markup, bool checkImageTags)
        {
            var linkUrls = GetAllUrlsFromHtmlDocument(markup, Constants.Html.LinkSearchPattern, Constants.Html.Href);
            if (checkImageTags)
            {
                var imgUrls = GetAllUrlsFromHtmlDocument(markup, Constants.Html.ImgSearchPattern, Constants.Html.Src);
                linkUrls.AddRange(imgUrls);
            }
            return linkUrls;
        }

        /// <summary>
        /// Get's a list of all urls in markup and tires to fix the urls that Restsharp will have a problem with 
        /// (i.e relative urls, urls with no sceme, mailto links..etc)
        /// </summary>
        /// <returns>List of urls that will work with restsharp for sending http get</returns>
        public static List<string> GetValidUrlListFromMarkup(string markup, IValidUrlParser parser, bool checkImages)
        {
            var urlList = GetAllUrlsFromMarkup(markup, checkImages);
            var validUrlList = new List<string>();

            foreach (var url in urlList)
            {
                string validUrl;
                if (parser.Parse(url, out validUrl))
                {
                    validUrlList.Add(validUrl);
                }
            }
            return validUrlList;
        }
    }
}
