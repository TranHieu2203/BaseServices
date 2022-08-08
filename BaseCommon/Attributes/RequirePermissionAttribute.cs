using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Base.Common.Attributes
{
    /// <summary>
    /// Class RequirePermissionAttribute
    /// </summary>
    public class RequirePermissionAttribute : IAsyncActionFilter
    {
        /// <summary>
        /// method event OnActionExecutionAsync
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //TODO:
            // 1. check the token to get user Role & Permission
            // 2. Check the permission whether a user can access the action or not

            var result = await next();

            //TODO: implement logic after the action is executed


        }
    }
}
