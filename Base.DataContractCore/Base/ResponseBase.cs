using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DataContractCore.Base
{
    public class ResponseBase
    {
        public string Message { set; get; }
        public string Code { set; get; }

        public object Data { set; get; }

        public ResponseBase() { }

        public ResponseBase(string code, string mess, object data)
        {
            Message = mess;
            Code = code;
            Data = data;
        }

        public ResponseBase(ResponseBase defaultRes, object data, string msg = "")
        {
            Code = defaultRes.Code;
            Message = string.IsNullOrEmpty(msg) ? defaultRes.Message : msg;
            Data = data;
        }
        public ResponseBase(ResponseBase defaultRes)
        {
            Code = defaultRes.Code;
            Message = defaultRes.Message;
        }
    }
}
