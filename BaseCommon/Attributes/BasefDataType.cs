using Base.DataContractCore.Exception;
using System.ComponentModel.DataAnnotations;

namespace Base.Common.Attributes
{
    public class BaseDataType : DataTypeAttribute
    {
        public BaseDataType(DataType dataType, string errMessage = "Datatype is not accepted") : base(dataType)
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
