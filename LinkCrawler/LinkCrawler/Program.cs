using System;
using System.Diagnostics;
using RestSharp;

namespace LinkCrawler
{
    class Program
    {
        static void Main2(string[] args)
        {
            var restClient = new RestClient("http://www.vg.no");
            var restRequest = new RestRequest(Method.GET);
            var restResponse = restClient.Execute(restRequest);
        }

       static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var crawler = new LinkCrawler();
            crawler.CrawlLinks();

            stopwatch.Stop();
            WriteFinishMessage(stopwatch);
        }

        private static void WriteFinishMessage(Stopwatch stopwatch)
        {
            var elapsedTime = stopwatch.Elapsed;
            var outputMessage = string.Format("____Finsihed____ {0}d {1}H {2}m {3}s", elapsedTime.Days, elapsedTime.Hours, elapsedTime.Minutes,
                elapsedTime.Seconds);
            Console.WriteLine(outputMessage);
        }
    }
}
