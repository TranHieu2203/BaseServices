using Base.DataContractCore.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DataContractCore.Constants
{
    public class DefaultMessage
    {
        public static readonly ResponseBase INTERNAL_SEVER_ERROR_MSG = new() { Code = "500", Message = "Internal server error" };
        public static readonly ResponseBase BAD_REQUEST_MSG = new() { Code = "400", Message = "The server could not understand the request due to invalid syntax." };
        public static readonly ResponseBase UNAUTHORIZE_MSG = new() { Code = "401", Message = "Unauthenticated" };
        public static readonly ResponseBase NOT_ALLOWED_MSG = new() { Code = "405", Message = "Method not allowed" };



        public static readonly ResponseBase SUCCESS_MSG = new() { Code = "200", Message = "Success" };
        public static readonly ResponseBase EXECUTE_ERROR_MSG = new() { Code = "601", Message = "The execution is error" };
        public static readonly ResponseBase ERROR_MSG = new() { Code = "600", Message = "Error" };


    }
}
