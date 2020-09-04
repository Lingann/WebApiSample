using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Contracts.Services;
using Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace Services.Authenciate
{
    public class AuthService : IAuthService
    {

        private readonly AuthSettings _authSettings;


        public AuthService(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public string GenerateJwtToken(User user)
        {
            if (user == null) return null;
            
            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Secret));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                
                // 生成的token校验保存7天
                Expires = DateTime.UtcNow.AddDays(7),

                SigningCredentials = signinCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
