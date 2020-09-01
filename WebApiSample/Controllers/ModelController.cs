using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiSample.Context;
using WebApiSample.Models;
namespace WebApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        ModelDbContext Context { get; set; }
        public ModelController(ModelDbContext context)
        {
            Context = context;
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
            Context.Models.Add(model);
            Context.SaveChanges();
            return model;
        }

        [HttpGet]
        public IEnumerable<Model> GetModels() => Context.Models.ToList();
      

    }
}
