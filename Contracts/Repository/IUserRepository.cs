using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
namespace Contracts.Service
{
    public interface IUserRepository
    {
        /// <summary>
        /// 用户校验
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        AuthenticateResponse Authenticate(string username, string password);
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetUsers();

        /// <summary>
        /// 通过UserId索引获取User信息
        /// </summary>
        /// <param name="id">id索引</param>
        /// <returns></returns>
        User GetUserById(int id);
    }
}
