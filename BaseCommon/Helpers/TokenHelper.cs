using Base.DataContractCore.Appsettings;
using Base.DataContractCore.Constants;
using Base.DataContractCore.Exception;
using Base.DataContractCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Base.Common.Helpers
{
    public static class TokenHelper
    {
        /// <summary>
        /// Generate Jwt token string from Login response object
        /// </summary>
        /// <param name="loginResponse"></param>
        /// <returns></returns>
        public static string GenerateJSONWebToken(LoginResponse loginResponse, AppType appType = AppType.WebApp)
        {
            var configuration = AppConfigHelper.AppSetting;

            var claims = GenerateClaimsIdentity(loginResponse);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JWTSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //get config of session timeout
            int minTimeout = 60;
            try
            {
                switch (appType)
                {
                    case AppType.Portal:
                        minTimeout = AppConfigHelper.AppSetting.Authentication.Find(m => m.Name.ToUpper() == "PORTAL").SessionExpiredMinutes;
                        break;
                    case AppType.Mobile:
                        minTimeout = AppConfigHelper.AppSetting.Authentication.Find(m => m.Name.ToUpper() == "MOBILE").SessionExpiredMinutes;
                        break;
                    case AppType.WebApp:
                        minTimeout = AppConfigHelper.AppSetting.Authentication.Find(m => m.Name.ToUpper() == "WEBAPP").SessionExpiredMinutes;
                        break;
                }
            }
            catch { }

            var tokenDescriptor = new JwtSecurityToken(configuration.JWTSettings.Issuer, configuration.JWTSettings.Audience, claims,
                expires: DateTime.Now.AddMinutes(minTimeout), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        /// <summary>
        /// GenerateClaimsIdentity use for api get user info 
        /// </summary>
        /// <param name="loginResponse"></param>
        /// <returns></returns>
        public static List<Claim> GenerateClaimsIdentity(LoginResponse loginResponse)
        {
            var claims = new List<Claim>
                    {
                        new Claim("UserId" , loginResponse.USER_ID),
                        new Claim(JwtRegisteredClaimNames.Name , loginResponse.USER_NAME),
                        new Claim(JwtRegisteredClaimNames.NameId  , loginResponse.EMPLOYEE_CODE??""),
                        new Claim(JwtRegisteredClaimNames.FamilyName  , loginResponse.FULL_NAME??""),
                        new Claim(JwtRegisteredClaimNames.Email  , loginResponse.EMAIL??""),
                        new Claim(JwtRegisteredClaimNames.UniqueName , loginResponse.TOKEN??"")
                    };
            //List<ClaimsIdentity> identities = new List<ClaimsIdentity>()
            //        {
            //            new ClaimsIdentity(claims)
            //        };
            return claims;
        }

        /// <summary>
        /// GetDataClaim and return object LoginResponse
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static LoginResponse GetDataClaim(HttpContext context)
        {
            if (context.User == null) throw new BaseUnauthorizeException();

            var rs = new LoginResponse()
            {
                USER_ID = context.User.FindFirst("UserId")?.Value,
                USER_NAME = context.User.FindFirst(JwtRegisteredClaimNames.Name)?.Value,
                EMPLOYEE_CODE = context.User.FindFirst(JwtRegisteredClaimNames.NameId)?.Value,
                FULL_NAME = context.User.FindFirst(JwtRegisteredClaimNames.FamilyName)?.Value,
                EMAIL = context.User.FindFirst(JwtRegisteredClaimNames.Email)?.Value,
                TOKEN = context.User.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value,
            };
            if (string.IsNullOrEmpty(rs.USER_NAME)) return null;
            return rs;
        }

        public static Task<string> GenerateJwtTokenForApp(AppType appType, int lifeTimeInMinutes, out DateTime expiresTime)
        {
            var authentications = AppConfigHelper.AppSetting.Authentication;
            var jwtSettings = AppConfigHelper.AppSetting.JWTSettings;

            var utcNow = DateTime.UtcNow;
            expiresTime = utcNow.AddMinutes(lifeTimeInMinutes);

            if (jwtSettings == null)
            {
                throw new BaseException($"Can not find configurations for jwt ");
            }

            AuthenticationSetting authenSetting = null;
            string appErr;
            switch (appType)
            {
                case AppType.WebApp:
                    authenSetting = authentications.Find(m => m.Name.ToUpper() == "WEBAPP");
                    appErr = "WebApp";
                    break;
                case AppType.Portal:
                    authenSetting = authentications.Find(m => m.Name.ToUpper() == "PORTAL");
                    appErr = "Portal";
                    break;
                case AppType.Mobile:
                    authenSetting = authentications.Find(m => m.Name.ToUpper() == "MOBILE");
                    appErr = "Mobile";
                    break;
                default:
                    throw new BaseException($"Can not find configurations for {appType}");
            }

            if (authenSetting == null)
            {
                throw new BaseException($"Can not find configurations for {appErr}");
            }

            var jwtPayLoad = new JwtPayload
            {
                { JwtRegisteredClaimNames.Iss, jwtSettings.Issuer },
                { JwtRegisteredClaimNames.Aud, jwtSettings.Audience },
                { JwtRegisteredClaimNames.Iat, (int)utcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds },
                { JwtRegisteredClaimNames.Exp, (int)expiresTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds }
            };

            var publicKey = authenSetting.PublicKey;
            if (publicKey == null)
            {
                throw new BaseException("Can not get public key for signature");
            }

            jwtPayLoad.AddClaim(new Claim("PublicKey", publicKey));

            var privateKey = authenSetting.PrivateKey;

            if (privateKey == null)
            {
                throw new BaseException("Can not get private key for signature");
            }
            var appName = authenSetting.AppName;
            jwtPayLoad.AddClaim(new Claim(ClaimTypes.NameIdentifier, appName != null ? appName : "not defined"));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));
            var jwtHeader = new JwtHeader(new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature));
            var jwtToken = new JwtSecurityToken(jwtHeader, jwtPayLoad);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwtToken));
        }
    }
}
