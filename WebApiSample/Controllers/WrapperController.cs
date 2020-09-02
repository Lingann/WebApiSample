using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiSample.Controllers
{
    [Route("api/wrapper")]
    [ApiController]
    public class WrapperController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;

        public WrapperController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        /// <summary>
        /// 获取所有的sinzone账户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var accounts = _repoWrapper.Account.FindByCondition(x => x.AccountType.Equals("sinzone"));
            var owner = _repoWrapper.Owner.FindAll();

            return new string[] { "value1", "value2"};
        }
    }
}
