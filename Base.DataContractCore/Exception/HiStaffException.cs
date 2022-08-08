using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DataContractCore.Exception
{
    public class BaseException : System.Exception
    {
        public const int StatusCode = 600;

        private const string DefaultMessage = "Internal Exception occurs";

        public BaseException() : base(DefaultMessage)
        {

        }
        public BaseException(string message) : base(message)
        {
            
        }

        public BaseException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

    }
}
