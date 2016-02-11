using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LinkCrawler.Utils;

namespace LinkCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncStreamWriter.InjectAsConsoleOut();

            var stopwatch = new Stopwatch();
            stopwatch.Start();  

            var crawler = new LinkCrawler();
            crawler.CrawlLinks();

            //Task.Run(async () => await crawler.CrawlLinks());

            stopwatch.Stop();
            WriteFinishMessage(stopwatch);
            Console.Read();
        }

        private static void WriteFinishMessage(Stopwatch stopwatch)
        {
            var elapsedTime = stopwatch.Elapsed;
            var outputMessage = string.Format("____Finsihed____ {0}d {1}H {2}m {3}s", elapsedTime.Days, elapsedTime.Hours, 
                                                                                      elapsedTime.Minutes, elapsedTime.Seconds);
            Console.WriteLine(outputMessage);
        }
    }
}
