using System;
using System.Collections.Generic;
using System.Text;
using Base.DataContractCore.Authentication;
using Base.DataContractCore.Metricts;
namespace Base.DataContractCore.Appsettings
{
    public class AppSettings
    {

        /// <summary>
        /// Host Origin is allowed with Cors
        /// </summary>
        public string AllowedHosts { get; set; }

        /// <summary>
        /// JWT Settings
        /// </summary>
        public JWTSetting JWTSettings { get; set; }

        /// <summary>
        /// Parameter config for prometheus
        /// </summary>
        public MetricsOptions MetricsOptions { get; set; }

        /// <summary>
        /// Các thông tin kết nối các loại DB
        /// </summary>
        public ConnectionSetting ConnectionSettings { set; get; }

        /// <summary>
        /// App authen config
        /// </summary>
        public List<AuthenticationSetting> Authentication { set; get; }

        public DataSettings DataSettings { set; get; }
    }
}
