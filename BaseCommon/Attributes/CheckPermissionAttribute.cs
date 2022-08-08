using Base.DataContractCore.Exception;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Common.Attributes
{
    /// <summary>
    /// Check permission to access to controller
    /// </summary>
    public class CheckPermissionAttribute : Attribute, IAsyncActionFilter
    {
        /// <summary>
        /// FunctionId
        /// </summary>
        public string FunctionId { get; set; }

        /// <summary>
        /// Action check permission
        /// </summary>
        public string Action { get; set; }


        /// <summary>
        /// OnActionExecutionAsync
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>

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
            //var _permissionBusiness = context.HttpContext.RequestServices.GetService(typeof(IGroupPermissionBusiness)) as GroupPermissionBusiness;
            //var id = decimal.Parse(claim.Value);
            //var getPermissionList = await _permissionBusiness.GetUserFunctionList(new GetUserPermissionListRequest
            //{
            //    USER_ID = (int)id
            //});

            //if (!getPermissionList.LIST_DATA.Any(x => FunctionId.Contains(x.FUNCTION_CODE)))
            //{
            //    throw new BaseUnauthorizeException();
            //}

            var result = await next();

        }
    }
}
