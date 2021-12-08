using LinkCrawler.Utils;
using LinkCrawler.Utils.Outputs;
using LinkCrawler.Utils.Parsers;
using LinkCrawler.Utils.Settings;
using StructureMap;
using System;
using System.Collections.Generic;

namespace LinkCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = Container.For<StructureMapRegistry>())
            {
                var linkCrawler = container.GetInstance<LinkCrawler>();
                linkCrawler.CheckImages = true;
                List<IOutput> outputs = new List<IOutput>();
                ISettings setting = new Settings();
                outputs.Add(new CsvOutput(setting));
                linkCrawler.Outputs = outputs.ToArray();
                if (args.Length == 0)
                {
                    linkCrawler.BaseUrl = "https://dallasinformationcenter.com/";
                }
                else if (args.Length > 0)
                {
                    string parsed;
                    var validUrlParser = new ValidUrlParser(new Settings());
                    var result = validUrlParser.Parse(args[0], out parsed);
                    if (result)
                        linkCrawler.BaseUrl = parsed;
                }
                linkCrawler.Start();
                Console.Read();
            }
        }
    }
}
