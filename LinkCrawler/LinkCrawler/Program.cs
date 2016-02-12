using System;

namespace LinkCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var crawler = new LinkCrawler();
            crawler.Start();
            Console.Read();
        }
    }
}
