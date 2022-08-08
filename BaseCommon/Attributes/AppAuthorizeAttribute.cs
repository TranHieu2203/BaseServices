using System;
using System.Linq;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Base.DataContractCore.Exception;
using Microsoft.AspNetCore.Mvc.Filters;
using Base.Common.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace Base.Common.Attributes
{
    /// <summary>
    /// Class attibute for authorize application 
    /// </summary>
    public class AppAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private static bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            return (allowAnonymous);
        }

        /// <summary>
        /// Method event OnAuthorization
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (HasAllowAnonymous(context)) return;

            //Check header key Authorization
            var sb = new StringBuilder();
            var headers = context.HttpContext.Request.Headers;

            var authorization = headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorization))
            {
                throw new BaseUnauthorizeException("Missing authorization");
            }
            sb.AppendLine($"\t{authorization}");
            if (!authorization.StartsWith("Bearer"))
            {
                throw new BaseUnauthorizeException("Schema not valid");
            }

            //Check content Header Authorization
            var token = authorization.Substring("Bearer".Length).Trim();
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            sb.AppendLine($"\tToken Payload: {jwtToken}");
            sb.AppendLine($"\tValid To: {jwtToken.ValidTo.ToLocalTime()}");

            //Check public key 
            if (!jwtToken.Payload.TryGetValue("PublicKey", out object publicKey))
            {
                throw new BaseUnauthorizeException("Missing public key");
            }

            var configuration = AppConfigHelper.AppSetting; //ConfigurationHelper.Configuration;
            var apps = configuration.Authentication;//configuration.GetSection("Authentication").GetChildren();
            var authenticatedApp = apps.FirstOrDefault(x => x.PublicKey == (string)publicKey);
            if (authenticatedApp == null)
            {
                throw new BaseUnauthorizeException("Public key not match any authentication data");
            }

            //ValidateToken
            var secretKey = authenticatedApp.PrivateKey;
            var options = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
            try
            {
                jwtHandler.ValidateToken(token, options, out var securityToken);
                context.RouteData.Values["AppName"] = authenticatedApp.AppName;
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message);
            }
        }

    }
}
