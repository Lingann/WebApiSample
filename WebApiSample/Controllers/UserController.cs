using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contracts.Service;
using Microsoft.AspNetCore.Authorization;
using Entities.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.Extensions.Logging;
using Contracts;
using Entities;

namespace WebApiSample.Controllers
{
    /// <summary>
    /// 用户控制器，定义并处理与用户相关的api的所有路由/端点，包括身份验证和标准CRUD操作，控制器都会调用用户服务以执行所需的操作，这使控制器保持“精简”状态，并与业务和数据访问代码完全隔离
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IRepositoryWrapper _repoWarpper;

        public UserController(IRepositoryWrapper repositoryWrapper )
        {
            _repoWarpper = repositoryWrapper;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromForm] AuthenticateRequest model)
        {
            var user = _repoWarpper.User.Authenticate(model.Username, model.Password);
             if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repoWarpper.User.GetUsers();
            return Ok(users);
        }


        [Authorize]
        [HttpGet("{id}")]
        public User GetUserById(int id)
        {
            return _repoWarpper.User.GetUserById(id);
        }

        [HttpGet("value")]
        public string GetValue()
        {
            return "value1";
        }
    }
}
