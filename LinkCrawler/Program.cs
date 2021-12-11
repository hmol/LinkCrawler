using static System.Console;

WriteLine("Starting the Crawler");
ISettings setting = new Settings();
List<IOutput> outputs = new();
outputs.Add(new CsvOutput(setting));
setting.Outputs = outputs;

//var validUrlParser = new ValidUrlParser(new Settings());
//if (args.Length == 0)
//{
//    setting.BaseUrl = "https://controlorigins.com/";
//}
//else if (args.Length > 0)
//{
//    var result = validUrlParser.Parse(args[0], out string parsed);
//    if (result)
//        setting.BaseUrl = parsed;
//}
//validUrlParser = new ValidUrlParser(setting);

var linkCrawler = new LinkCrawler.Domain.LinkCrawler(setting);
WriteLine($"Crawler Configured BaseURL:{setting.BaseUrl}");
linkCrawler.Start();
WriteLine($"Crawler Complete ");
ReadLine();
