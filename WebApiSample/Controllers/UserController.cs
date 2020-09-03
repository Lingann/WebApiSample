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

namespace WebApiSample.Controllers
{
    /// <summary>
    /// 用户控制器，定义并处理与用户相关的api的所有路由/端点，包括身份验证和标准CRUD操作，控制器都会调用用户服务以执行所需的操作，这使控制器保持“精简”状态，并与业务和数据访问代码完全隔离
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]User userParam)
        {
            var user = await _userRepository.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }

        
    }
}
