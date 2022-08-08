using System;
using System.Collections.Generic;
using System.Text;
using Base.DataContractCore.Appsettings;
using Newtonsoft.Json;

namespace Base.Common.Helpers
{

    /// <summary>
    /// Init from Startup.cs 
    /// Read appsettings.json and convert to object
    /// </summary>
    public class AppConfigHelper
    {
        private static AppSettings _appSettings = new AppSettings();

        public static AppSettings AppSetting { get { return _appSettings; } set { _appSettings = value; } }


        public static void ConvertSetting(string jsonSetting)
        {
            _appSettings = JsonConvert.DeserializeObject<AppSettings>(jsonSetting);
        }
    }
}
