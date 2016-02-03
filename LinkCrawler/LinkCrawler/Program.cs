using System;
using System.IO;
using System.Security.Policy;

namespace LinkCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var crawler = new LinkCrawler();
            crawler.CrawlLinks();
            Console.ReadLine();
        }
    }
}
