namespace Base.DataContractCore.Appsettings
{
    public class AuthenticationSetting
    {
        public string Name { set; get; }
        public string AppName { set; get; }
        public int AppExpiredDays { set; get; }
        public int SessionExpiredMinutes { set; get; }
        public string PublicKey{ set; get; }
        public string PrivateKey{ set; get; }

    }
}
