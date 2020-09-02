using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Entities.Models;

namespace WebApiSample.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;

        public AccountController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        /// <summary>
        /// 获取账户
        /// </summary>
        /// <param name="id">账户id索引</param>
        /// <returns></returns>
        [HttpGet]
        public Account GetAccount(int id)
        {
            return _repoWrapper.Account.GetAccountById(id);
        }

        /// <summary>
        /// 获取Owner下的所有账户
        /// </summary>
        /// <param name="ownerId">owner Id索引</param>
        /// <returns></returns>
        [HttpGet("GetAccountsByOwnerId/{ownerId}")]
        public ICollection<Account> GetAccountsByOwnerId(int ownerId)
        {
            return _repoWrapper.Account.GetAccountsByOwnerId(ownerId);
        }
    }
}
