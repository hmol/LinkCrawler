using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCrawler
{
    public static class Settings
    {
        public static string BaseUrl
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettings.BaseUrl]; }
        }

        public static string WriteToFile
        {
            get
            {
                return ConfigurationManager.AppSettings[Constants.AppSettings.WriteToFile];
            }
        }
    }
}
