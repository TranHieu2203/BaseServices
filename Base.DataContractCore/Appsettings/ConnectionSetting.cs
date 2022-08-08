namespace Base.DataContractCore.Appsettings
{
    public class ConnectionSetting
    {
        /// <summary>
        /// Encrypted key to encrypte connection string
        /// </summary>
        public string SecretKey { set; get; }

        /// <summary>
        /// Define database type (Oracle, MsSql)
        /// </summary>
        public string DBType { set; get; }

        /// <summary>
        /// Connection strings of DB (case read/write)
        /// </summary>
        public ConnectionString ConnectionStrings { set; get; }

        public class ConnectionString
        {

            /// <summary>
            /// String of connection - read
            /// </summary>
            public string ReadConnectString { set; get; }

            /// <summary>
            /// String of connection - write
            /// </summary>
            public string WriteConnectString { set; get; }

            /// <summary>
            /// String of connection - system
            /// </summary>
            public string SystemConnectString { set; get; }
        }

    }
}
