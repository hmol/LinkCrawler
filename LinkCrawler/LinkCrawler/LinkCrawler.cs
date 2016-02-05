using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkCrawler.Models;
using LinkCrawler.Utils.Helpers;

namespace LinkCrawler
{
    public class LinkCrawler
    {
        public string BaseUrl;
        public static List<string> VisitedUrlList;
        public StringBuilder StringBuilder;

        public LinkCrawler()
        {
            BaseUrl = Settings.BaseUrl;
            VisitedUrlList = new List<string>();
            StringBuilder = new StringBuilder();
        }

        public void CrawlLinks()
        {
            CrawlLink(BaseUrl);
        }

        public void CrawlLink(string crawlUrl, string referrerUrl = "")
        {
            var linkItem = new LinkItem(crawlUrl);
            var responseItem = linkItem.SendRequestAndGetMarkup();

            Console.WriteLine(responseItem);

            if (!responseItem.IsSucess)
            {
                Console.WriteLine("Reffered in: " + referrerUrl);
                return;
            }

            var linksFoundInMarkup = GetListOfUrls(responseItem.Markup);
            
            foreach (var url in linksFoundInMarkup)
            {
                if (VisitedUrlList.Contains(url))
                    continue;

                VisitedUrlList.Add(url);
                CrawlLink(url, crawlUrl);
            }
        }

        public List<string> GetListOfUrls(string markup)
        {
            var urlList = StringHelpers.GetUrlListFromMarkup(markup);
            var cleanUrlList = new List<string>();

            foreach (var url in urlList)
            {
                Uri parsedUri;
                if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out parsedUri))
                    continue;

                if(!parsedUri.IsAbsoluteUri)
                {
                    var newUrl = BaseUrl + url;
                    cleanUrlList.Add(newUrl);
                }
                else if (parsedUri.AbsoluteUri.StartsWith(BaseUrl))
                {
                    cleanUrlList.Add(url);
                }
            }

            return cleanUrlList;
        }
    }
}