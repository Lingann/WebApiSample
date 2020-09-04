using Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Authenciate;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerService;
using Contracts;
using Entities.Models;

namespace WebApiSample.Helpers
{
    /// <summary>
    /// 自定义JWT中间件
    /// 自定义JWT中间件检查请求Authorization标头中是否有令牌，如果有，则尝试
    /// 1、 验证令牌
    /// 2、 从令牌中提取用户ID
    /// 3、 将经过身份验证的用户附加到当前HttpContext.Items集合，以使其在当前请求的范围内可访问
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthSettings _authSettings;
        public JwtMiddleware(RequestDelegate next, IOptions<AuthSettings> authSettings)
        {
            _next = next;
            _authSettings = authSettings.Value;
        }

        public async Task Invoke(HttpContext context, IAuthService authService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split("").Last();

            if (token != null) 
                AttachAuthToContext(context, authService, token);


            await _next(context);
        }

        private void AttachAuthToContext(HttpContext context, IAuthService authService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.UTF8.GetBytes(_authSettings.Secret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters {

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = false,

                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                Console.WriteLine("发生了什么");

                context.Items["User"] = new User()
                {
                    Id = 1,
                    FirstName = "lingann",
                    LastName = "wang",
                    Username = "admin",
                    Password = "123456"
                };
            }
            catch
            {

            }
        }
    }
}
