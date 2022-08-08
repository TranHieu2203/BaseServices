using Base.Common.Helpers;
using Base.DataContractCore.Base;
using Base.DataContractCore.Constants;
using Base.DataContractCore.Exception;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Base.Common.Middleware
{

    /// <summary>
    /// Use Middleware for exception
    /// </summary>
    public static class ExceptionMiddleware
    {
        /// <summary>
        /// Config Exception handle
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var errorResp = DefaultMessage.INTERNAL_SEVER_ERROR_MSG;

                    // filter type of exceptions
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exceptionType = contextFeature.Error.GetType();
                    if (exceptionType == typeof(BaseUnauthorizeException))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        errorResp = DefaultMessage.UNAUTHORIZE_MSG;
                        LogHelper.WriteAuthorizeLog("The request is unauthorized", contextFeature.Error);
                        context.Response.Cookies.Delete(CookieName.ClientAuthorize);
                    }
                    else if (exceptionType == typeof(BaseExpiredTokenException))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        errorResp = new ResponseBase(DefaultMessage.UNAUTHORIZE_MSG, "", "Token is expired");
                        LogHelper.WriteAuthorizeLog("Token is expired", contextFeature.Error);
                        context.Response.Cookies.Delete(CookieName.ClientAuthorize);
                    }
                    else if (exceptionType == typeof(BaseException))
                    {
                        context.Response.StatusCode = 600;
                        errorResp = new ResponseBase(DefaultMessage.INTERNAL_SEVER_ERROR_MSG, "", contextFeature.Error.Message);
                        LogHelper.WriteExceptionToLog("CatchExceptionByBase: " + errorResp.Message, contextFeature.Error);
                    }
                    else if (exceptionType == typeof(BaseDatabaseException))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResp = new ResponseBase(DefaultMessage.INTERNAL_SEVER_ERROR_MSG, "", contextFeature.Error.Message);
                        LogHelper.WriteSystemLog("Execute command is corrupted", contextFeature.Error);
                    }
                    else if (exceptionType == typeof(BaseInvalidException))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResp = new ResponseBase(DefaultMessage.BAD_REQUEST_MSG, "", contextFeature.Error.Message);
                        LogHelper.WriteSystemLog("ValidateException", contextFeature.Error);
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResp.Message = contextFeature.Error?.Message;
                        LogHelper.WriteExceptionToLog("OtherException: " + (contextFeature.Error?.Message), contextFeature.Error);
                    }
                    context.Response.ContentType = "application/json";

                    //todo: log here
                    if (string.IsNullOrEmpty(errorResp.Message))
                        //errorResp.Message = context.Response.StatusCode == 500 ? DefaultMessage.INTERNAL_SEVER_ERROR_MSG.Message : contextFeature.Error.Message;
                        errorResp.Message = contextFeature.Error.Message;

                    var strErr = JsonConvert.SerializeObject(errorResp, new JsonSerializerSettings()
                    {
                        ContractResolver = new LowercaseContractResolver()
                    });

                    try
                    {
                        await context.Response.WriteAsync(strErr);
                    }
                    catch (Exception ext)
                    {
                        LogHelper.WriteExceptionToLog(ext);
                    }
                });
            });
        }
    }
}
