<<<<<<< Updated upstream
﻿using System.Configuration;
using LinkCrawler.Utils.Extensions;
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> Stashed changes

namespace LinkCrawler
{
    public class Settings
    {
<<<<<<< Updated upstream
        public static string BaseUrl
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettings.BaseUrl]; }
        }

        public static bool CheckImages
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettings.CheckImages].ToBool(); }
=======
        private static Settings _instance;
        public static Settings Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                return _instance = new Settings();
            }
>>>>>>> Stashed changes
        }

        public Settings()
        {
            
        }

        public string BaseUrl
        {
            get { return ConfigurationManager.AppSettings[Constants.AppSettings.BaseUrl]; }
        }
    }
}
