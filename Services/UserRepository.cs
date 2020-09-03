using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Service;
using Entities.Models;

namespace Services
{
    public class UserService : IUserService
    {
        // 为了简单起见，对User进行硬编码，并在生产应用程序中使用哈希密码将其存储在数据库中
        private List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Username = "test",
                Password = "test"
            }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            if (user == null) return null;

            // 校验成功后，将不带有密码的用户详情进行返回
            user.Password = null;
            
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Task.Run(() => _users.Select(x =>
            {
                x.Password = null;
                return x;
            }));
        }
    }
}
