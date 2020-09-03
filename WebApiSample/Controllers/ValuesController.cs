using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApiSample.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly ILoggerManager _logger;

        public ValuesController(ILoggerManager logger)
        {
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        /// <summary>
        /// 获取值信息
        /// </summary>
        /// <param name="id">id值</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            _logger.LogInfo("Here is info message from the controller.");
            _logger.LogDebug("Here is debug message from the controller.");
            _logger.LogWarn("Here is warn message from the controller.");
            _logger.LogError("Here is error message from the controller.");
            return "value   ${id}";
        }

        // POST api/values
        /// <summary>
        /// 获取值信息
        /// </summary>
        /// <param name="value">值信息</param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        /// <summary>
        /// 获取值信息
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="value">value</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        /// <summary>
        /// 值删除
        /// </summary>
        /// <param name="id">id</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
