using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.Models;
namespace WebApiSample.Controllers
{
    [Route("api/model")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private RepositoryContext _repoContext;
        public ModelController(RepositoryContext repoContext)
        {
            _repoContext = repoContext;
        }

        /// <summary>
        /// 添加模型
        /// </summary>
        /// <remarks>
        ///  示例:/n
        ///
        ///     Post api/model
        ///     {
        ///          "id" : "1235",
        ///          "name": "钢铁侠",
        ///           "url": "alioss.shenzhen.buket.ironman.fbx"
        ///     }
        /// </remarks>
        /// <param name="model">模型名称</param>
        /// <returns></returns>
        [HttpPost]
        public Model AddModel([FromForm]Model model)
        {
            _repoContext.Models.Add(model);
            _repoContext.SaveChanges();
            return model;
        }

        /// <summary>
        /// 获取所有模型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Model> GetModels() => _repoContext.Models.ToList();
      

    }
}
