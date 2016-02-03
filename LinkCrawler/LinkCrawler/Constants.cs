using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkCrawler
{
    public static class Constants
    {
        public static class AppSettings
        {
            public const string BaseUrl = "BaseUrl";
            public const string WriteToFile = "WriteToFile";
        }

        public static class Html
        {
            public const string Mailto = "mailto";
            public const string Href = "href";
            public const string LinkSearchPattern = "//a[@href]";
        }
    }
}
