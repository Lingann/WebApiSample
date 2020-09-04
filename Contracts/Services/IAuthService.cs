using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GenerateJwtToken(User user);
    }
}
