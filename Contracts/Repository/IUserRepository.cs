using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
namespace Contracts.Service
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetUsers();
    }
}
