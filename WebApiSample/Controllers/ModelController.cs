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
