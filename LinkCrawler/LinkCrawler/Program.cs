using LinkCrawler.Utils;
using StructureMap;
using System;
using LinkCrawler.Utils.Parsers;
using LinkCrawler.Utils.Settings;

namespace LinkCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            
            using (var container = Container.For<StructureMapRegistry>())
            {
                var linkCrawler = container.GetInstance<LinkCrawler>();
                Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["FollowRedirects"]);
                if (args.Length >0)
                {
                    string parsed;
                    var validUrlParser = new ValidUrlParser(new Settings());
                    var result = validUrlParser.Parse(args[0], out parsed);
                    if(result)
                    linkCrawler.BaseUrl = parsed;
                }
                linkCrawler.Start();
                Console.Read();
            }
        }
    }
}
