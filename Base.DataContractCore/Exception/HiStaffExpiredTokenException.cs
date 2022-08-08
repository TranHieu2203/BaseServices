using System;

namespace Base.DataContractCore.Exception
{
    public class BaseExpiredTokenException: System.Exception
    {
        public const int StatusCode = 402;

        private const string DefaultMessage = "Token is expired";
        public BaseExpiredTokenException() : base(DefaultMessage)
        {

        }
        public BaseExpiredTokenException(string message) : base(message)
        {

        }

        public BaseExpiredTokenException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
