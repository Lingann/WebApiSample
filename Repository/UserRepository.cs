using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Contracts.Service;
using Entities;
using Entities.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Services
{
    public class UserRepository : IUserRepository
    {
        private RepositoryContext _repoContext;

        public UserRepository(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _repoContext.Users.SingleOrDefault(x => x.Username == username && x.Password == password));

            if (user == null) return null;

            // 校验成功后，将不带有密码的用户详情进行返回
            user.Password = null;
            
            return user;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Task.Run(() => _repoContext.Users.Select( x => new User()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Username = x.Username
            }));
        }

        
        /// <summary>
        /// 通过Id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            return _repoContext.Users.SingleOrDefault(x => x.Id == id);
        }

        private string GenerateJwtToken(User user)
        {
            if (user == null) return null;

            // 生成的token校验保存7天
            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lingann.m@gmail.com"));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = signinCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }


    }
}
