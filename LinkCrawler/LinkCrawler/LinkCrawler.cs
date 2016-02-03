using System;
using System.Collections.Generic;
using System.Text;
using LinkCrawler.Models;
using LinkCrawler.Utils.Helpers;

namespace LinkCrawler
{
    public class LinkCrawler
    {
        public string BaseUrl;
        public bool WriteToFile;
        public string FilePath;
        public static List<string> VisitedUrlList;
        public StringBuilder StringBuilder;

        public LinkCrawler()
        {
            BaseUrl = Settings.BaseUrl;
            FilePath = Settings.WriteToFile;
            WriteToFile = !string.IsNullOrEmpty(Settings.WriteToFile);
            VisitedUrlList = new List<string>();
            StringBuilder = new StringBuilder();
        }

        public void CrawlLinks()
        {
            CrawlLink(BaseUrl);
        }

        public void CrawlLink(string crawlUrl)
        {
            var linkItem = new LinkModel(crawlUrl);
            linkItem.SendRequestAndGetMarkup();
            Console.WriteLine(linkItem);
            if (!linkItem.IsSucess)
                return;

            var linksFoundInMarkup = GetListOfUrls(linkItem.Markup);

            foreach (var url in linksFoundInMarkup)
            {
                if (VisitedUrlList.Contains(url))
                    continue;

                VisitedUrlList.Add(url);
                CrawlLink(url);
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