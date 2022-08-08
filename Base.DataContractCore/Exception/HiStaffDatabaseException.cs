using System;

namespace Base.DataContractCore.Exception
{
    public class BaseDatabaseException : System.Exception
    {
        public const int StatusCode = 610;

        private const string DefaultMessage = "Command is corrupted";

        public BaseDatabaseException() : base(DefaultMessage)
        {

        }
        public BaseDatabaseException(string message) : base(message)
        {

        }

        public BaseDatabaseException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

    }
}
