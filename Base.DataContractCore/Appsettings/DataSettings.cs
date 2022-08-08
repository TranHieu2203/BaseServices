namespace Base.DataContractCore.Appsettings
{
    public class DataSettings
    {
        /// <summary>
        /// Datetime format - json parse and serilize transfer client - server
        /// </summary>
        public string DateTimeFormat { set; get; }

        /// <summary>
        /// init default
        /// </summary>
        public DataSettings()
        {
            DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
        }
    }
}
