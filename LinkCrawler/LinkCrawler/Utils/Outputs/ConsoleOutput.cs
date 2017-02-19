using System;
using LinkCrawler.Models;
using LinkCrawler.Utils.Helpers;

namespace LinkCrawler.Utils.Outputs
{
    public class ConsoleOutput : IOutput
    {
        public void WriteError(IResponseModel responseModel)
        {
            ConsoleHelper.WriteError(responseModel.ToString());
        }

        public void WriteInfo(IResponseModel responseModel)
        {
            WriteInfo(new string[] { responseModel.ToString() });
        }

        public void WriteInfo(String[] Info)
        {
            foreach(string line in Info) Console.WriteLine(line);
        }
    }
}
