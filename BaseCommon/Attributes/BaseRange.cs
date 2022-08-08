using Base.DataContractCore.Exception;
using System.ComponentModel.DataAnnotations;

namespace Base.Common.Attributes
{
    public class BaseRange : RangeAttribute
    {
        public BaseRange(double minimum, double maximum, string errMessage = "Value is not in range") : base(minimum, maximum)
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
