namespace Base.DataContractCore.Exception
{
    public class BaseInvalidException: System.Exception
    {
        public const int StatusCode = 400;

        private const string DefaultMessage = "Invalid data exception";

        public BaseInvalidException() : base(DefaultMessage)
        {

        }
        public BaseInvalidException(string message) : base(message)
        {

        }

        public BaseInvalidException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
