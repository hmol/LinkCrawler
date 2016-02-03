using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkCrawler
{
    public class Constants
    {
        public class AppSettings
        {
            public const string BaseUrl = "BaseUrl";
        }

        public class LinkAttributes
        {
            public const string Mailto = "mailto";
            public const string Href = "href";
            public const string LinkSearchPattern = "//a[@href]";
        }
    }
}
