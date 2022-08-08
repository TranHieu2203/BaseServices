using Base.Common.Helpers;
using Base.DataContractCore.Appsettings;
using Base.DataContractCore.Constants;
using Base.DataContractCore.Exception;
using Base.DataContractCore.Authentication;
using Base.System.Business.Authenticate;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Base.System.Business.AuditAction;

namespace Base.Common.Middleware
{
    /// <summary>
    /// JWT middleware
    /// </summary>
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Contructor
        /// </summary>
        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userService"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IAuthenticateBusiness userService, IAuditActionBusiness auditAction)
        {
            //Check anonymous

            //get cookie info (token)
            var token = context.Request.Cookies[CookieName.ClientAuthorize]?.Split(" ").Last();
            if (string.IsNullOrEmpty(token)) token = context.Request.Headers["Authorization"].ToString().Split(" ").Last();
            //check and valid cookie
            if (!string.IsNullOrEmpty(token)) attachUserToContext(context, userService, token, auditAction);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IAuthenticateBusiness userService, string token, IAuditActionBusiness auditAction)
        {
            var sb = new StringBuilder();
            var authorization = token;

            if (string.IsNullOrEmpty(authorization))
            {
                throw new BaseUnauthorizeException("Missing authorization");
            }

            sb.AppendLine($"\t{authorization}");

            //var token = authorization.Substring("Bearer".Length).Trim();
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            sb.AppendLine($"\tToken Payload: {jwtToken}");
            sb.AppendLine($"\tValid To: {jwtToken.ValidTo.ToLocalTime()}");

            //get jwt setting from appconfig.json
            var jwtSettings = AppConfigHelper.AppSetting.JWTSettings; //ConfigurationHelper.Configuration.GetSection("JWTSettings").GetChildren();

            // TODO : check the user id in the db with the token ..
            var rsCheckToken = userService.CheckAndGetDataToken(authorization).Result;
            if (rsCheckToken.Code == DefaultMessage.SUCCESS_MSG.Code)
            {
                if (rsCheckToken.Code == DefaultMessage.UNAUTHORIZE_MSG.Code) throw new BaseUnauthorizeException("No token found");
                if (rsCheckToken.Code != DefaultMessage.SUCCESS_MSG.Code) throw new BaseUnauthorizeException("Data token is expired");

                //Convert data 
                var userInfo = (LoginResponse)rsCheckToken.Data;

                var secretKey = jwtSettings.SecretKey;

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

                    var claims = TokenHelper.GenerateClaimsIdentity(userInfo);
                    List<ClaimsIdentity> identities = new List<ClaimsIdentity>() { new ClaimsIdentity(claims) };

                    //context.User use for API with user info
                    context.User = new ClaimsPrincipal(identities);

                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        throw new BaseExpiredTokenException("Exception on JWT middle ware: SecurityTokenExpiredException", ex);
                    }
                    throw new BaseExpiredTokenException("Exception on JWT middle ware: ", ex);
                }
            }
            else if (!string.IsNullOrEmpty(token))
            {
                context.Response.Cookies.Delete(CookieName.ClientAuthorize);
                throw new BaseUnauthorizeException("Data token is expired");
            }
        }
    }
}
