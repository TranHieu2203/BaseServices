using Base.DataContractCore.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Common.Attributes
{
    /// <summary>
    /// Class ClientAuthorizeAttribute
    /// </summary>
    public class ClientAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private static bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            var filters = context.Filters;
            var rs = false;
            rs = filters.OfType<IAllowAnonymousFilter>().Any();
            if (rs) return rs;

            var endpoint = context.ActionDescriptor.EndpointMetadata;
            if ((bool)(endpoint?.OfType<BaseAllowAnonymousAttribute>().Any()))
            {
                return true;
            }
            return rs;
        }


        /// <summary>
        /// Method event authorize
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (HasAllowAnonymous(context)) return;

            //TODO'
            //Check if have no cookie -> add default to middleware check it
            //get cookie info (token)
            var token = context.HttpContext.Request.Cookies[CookieName.ClientAuthorize]?.Split(" ").Last();
            if (string.IsNullOrEmpty(token)) token = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(token)) throw new DataContractCore.Exception.BaseUnauthorizeException("Missing client authorize");

            if (token == "" && context.HttpContext.User.Claims.First().Value == null) throw new DataContractCore.Exception.BaseUnauthorizeException("Missing client authorize");

        }
    }
}
