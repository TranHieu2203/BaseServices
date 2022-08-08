using System;

namespace Base.DataContractCore.Exception
{
    public class BaseUnauthorizeException: System.Exception
    {
        public const int StatusCode = 401;

        private const string DefaultMessage = "Not Authorized";
        public BaseUnauthorizeException() : base(DefaultMessage)
        {

        }
        public BaseUnauthorizeException(string message) : base(message)
        {

        }

        public BaseUnauthorizeException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
