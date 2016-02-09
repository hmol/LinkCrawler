using System.Configuration;
using LinkCrawler.Utils.Extensions;

namespace LinkCrawler
{
    public class Settings
    {
        private static Settings _instance;
        public static Settings Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                return _instance = new Settings();
            }
        }

        public string BaseUrl
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettings.BaseUrl]; }
        }

        public bool CheckImages
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettings.CheckImages].ToBool(); }
        }
    }
}
