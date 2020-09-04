using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Contracts.Service;
using Entities;
using Entities.Models;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Services.Authenciate;
using Contracts.Services;
namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private RepositoryContext _repoContext;
       private IAuthService _authService;
        public UserRepository(RepositoryContext repositoryContext, IAuthService authService)
        {
            _repoContext = repositoryContext;
            _authService = authService;
        }

        /// <summary>
        /// 用户校验
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public AuthenticateResponse Authenticate(string username, string password)
        {
            var user = _repoContext.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null) return null;

            // 校验成功后，将不带有密码的用户详情进行返回
            var token = _authService.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
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
        public User GetUserById(int id)
        {
            return _repoContext.Users.SingleOrDefault(x => x.Id == id);
        }
    }
}
