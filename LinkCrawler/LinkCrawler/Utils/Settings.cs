using System.Configuration;
using LinkCrawler.Utils.Extensions;

namespace LinkCrawler.Utils
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

        public string BaseUrl => 
            ConfigurationManager.AppSettings[Constants.AppSettings.BaseUrl].Trim('/');

        public string ValidUrlRegex => 
            ConfigurationManager.AppSettings[Constants.AppSettings.ValidUrlRegex];

        public bool CheckImages => 
            ConfigurationManager.AppSettings[Constants.AppSettings.CheckImages].ToBool();

        public bool OnlyReportBrokenLinksToOutput => 
            ConfigurationManager.AppSettings[Constants.AppSettings.OnlyReportBrokenLinksToOutput].ToBool();

        public string SlackWebHookUrl => 
            ConfigurationManager.AppSettings[Constants.AppSettings.SlackWebHookUrl];

        public string SlackWebHookBotName => 
            ConfigurationManager.AppSettings[Constants.AppSettings.SlackWebHookBotName];

        public string SlackWebHookBotIconEmoji => 
            ConfigurationManager.AppSettings[Constants.AppSettings.SlackWebHookBotIconEmoji];

        public string SlackWebHookBotMessageFormat =>
            ConfigurationManager.AppSettings[Constants.AppSettings.SlackWebHookBotMessageFormat];
    }
}
