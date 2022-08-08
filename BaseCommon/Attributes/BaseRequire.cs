using Base.DataContractCore.Exception;
using System.ComponentModel.DataAnnotations;

namespace Base.Common.Attributes
{
    public class BaseRequired : RequiredAttribute
    {
        public BaseRequired(string errMessage = "Data field is required")
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
