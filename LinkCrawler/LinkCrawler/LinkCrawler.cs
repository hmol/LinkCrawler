using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

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

        public string GetMarkup(string url)
        {
            try
            {
                string html;

                var request = (HttpWebRequest) WebRequest.Create(url);
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    Console.WriteLine(url + " ==> " + response.StatusCode);
                    if (response.StatusCode != HttpStatusCode.OK)
                        return string.Empty;

                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                }

                return html;
            }
            catch (WebException ex)
            {
                Console.WriteLine(url + " ==> " + (ex.Response as HttpWebResponse).StatusCode);
                return string.Empty;
            }
        }
        public void CrawlLink(string crawlUrl)
        {
            
            try
            {
                var html = GetMarkup(crawlUrl);
                var linksFoundInMarkup = GetLinks(html);

                foreach (var url in linksFoundInMarkup)
                {
                    if(VisitedUrlList.Contains(url))
                        continue;

                    VisitedUrlList.Add(url);
                    CrawlLink(url);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        private static HtmlNodeCollection GetHtmlNodes(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc.DocumentNode.SelectNodes(Constants.Html.LinkSearchPattern);
        }
        private List<string> GetLinks(string html)
        {
            if(string.IsNullOrEmpty(html))
                return new List<string>();

            var nodes = GetHtmlNodes(html);

            if(nodes == null || !nodes.Any())
                return new List<string>();

            var urlList = nodes.Select(x => x.GetAttributeValue(Constants.Html.Href, string.Empty).TrimEnd('/')).ToList();
            var cleanUrlList = new List<string>();

            foreach (var url in urlList)
            {
                if (!url.StartsWith("/")
                    && !url.StartsWith(BaseUrl))
                    continue;

                if (url.StartsWith("/"))
                {
                    var newUrl = BaseUrl + url;
                    cleanUrlList.Add(newUrl);
                }
                else
                {
                    cleanUrlList.Add(url);
                }
            }

            return cleanUrlList;
        }
    }
}
