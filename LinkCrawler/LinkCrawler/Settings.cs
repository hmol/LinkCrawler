using System.Configuration;
using LinkCrawler.Utils.Extensions;

namespace LinkCrawler
{
    public static class Settings
    {
        public static string BaseUrl
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettings.BaseUrl]; }
        }

        public static bool CheckImages
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettings.CheckImages].ToBool(); }
        }
    }
}
