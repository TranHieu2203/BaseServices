using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DataContractCore.Authentication
{
    public class JWTSetting
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
