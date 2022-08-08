using Base.DataContractCore.Exception;
using System.ComponentModel.DataAnnotations;

namespace Base.Common.Attributes
{
    public class BaseStringLength : StringLengthAttribute
    {
        public BaseStringLength(int maxLength, string errMessage = "Maximum length") : base(maxLength)
        {
            ErrorMessage = errMessage;
        }
        public override bool IsValid(object value)
        {
            var rs = base.IsValid(value);
            if (!rs) throw new BaseInvalidException(ErrorMessage);
            return rs;
        }
    }
}
