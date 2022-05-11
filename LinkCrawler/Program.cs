using static System.Console;

WriteLine("Starting the Crawler");
ISettings setting = new Settings();
List<IOutput> outputs = new();
outputs.Add(new CsvOutput(setting));
setting.Outputs = outputs;
var linkCrawler = new LinkCrawler.Domain.LinkCrawler(setting);
WriteLine($"Crawler Configured BaseURL:{setting.BaseUrl}");
linkCrawler.Start();
WriteLine($"Crawler Complete ");
ReadLine();
