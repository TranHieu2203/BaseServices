using Base.Common.Helpers;
using Base.DataContractCore.Exception;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Common.Attributes
{
    public class AuditActionAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;
            if (user == null)
            {
                throw new BaseUnauthorizeException();
            };
            var claim = user.Claims.FirstOrDefault(x => x.Type == "UserId");
            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                throw new BaseUnauthorizeException();
            }

            //var auditAction = context.HttpContext.RequestServices.GetService(typeof(IAuditActionBusiness)) as AuditActionBusiness;
            //var userInfo = TokenHelper.GetDataClaim(context.HttpContext);

            //Log action by request
            //if (auditAction != null)
            //{
            //    try
            //    {
            //        _ = auditAction.InsertAuditAction(new System.DataContract.AuditAction.SE_ACTION_LOG()
            //        {
            //            ACCESS_TIME = DateTime.Now.ToString(AppConfigHelper.AppSetting.DataSettings.DateTimeFormat),
            //            ACTION_DATE = DateTime.Now,
            //            USERNAME = userInfo.USER_NAME,
            //            IP = context.HttpContext.Connection.RemoteIpAddress.ToString(),
            //            EMAIL = userInfo.EMAIL,
            //            DEVICE_INFO = context.HttpContext.Request.Headers["User-Agent"].ToString(),
            //            DEVICE_TYPE = "BROWSER",
            //            ACTION_NAME = context.HttpContext.Request.Path,
            //            ACTION_TYPE = context.HttpContext.Request.Method
            //        });
            //    }
            //    catch { }
            //}

            var result = next();

        }
    }
}
