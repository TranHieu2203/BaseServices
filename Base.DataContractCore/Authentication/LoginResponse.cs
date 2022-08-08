
namespace Base.DataContractCore.Authentication
{
    public class LoginResponse
    {
        public string USER_ID { set; get; }
        public string USER_NAME { set; get; }
        public string FULL_NAME { set; get; }
        public string EMAIL { set; get; }
        public string EMPLOYEE_CODE { set; get; }
        
        public string TOKEN { set; get; }
    }
}
