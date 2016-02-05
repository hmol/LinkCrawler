using System.Configuration;

namespace LinkCrawler
{
    public static class Settings
    {
        public static string BaseUrl
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettings.BaseUrl]; }
        }
    }
}
