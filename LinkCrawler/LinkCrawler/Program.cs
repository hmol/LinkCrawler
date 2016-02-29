using LinkCrawler.Utils;
using StructureMap;
using System;

namespace LinkCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = Container.For<StructureMapRegistry>())
            {
                var linkCrawler = container.GetInstance<LinkCrawler>();
                linkCrawler.Start();
                Console.Read();
            }
        }
    }
}
