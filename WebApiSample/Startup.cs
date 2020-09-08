using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApiSample.Extensions;
using Services.Authenciate;
namespace WebApiSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // 该方法用于服务注册
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // 配置跨域服务
            services.ConfigureCors();
            // 配置IIS
            services.ConfigureIISIntegration();
            // 配置Swagger
            services.ConfigureSwaggerGen();
            // 连接数据库
            services.ConfigureSqliteContext();
            // 添加存储库
            services.ConfigureRepositoryWrapper();
            // 配置日志
            services.ConfigureLoggerService();
            // 配置JWT
            services.ConfigureAuthentication(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            // 通过ASP.NET Core配置授权认证，读取客户端中的身份标识(Cookie,Token等)并解析出来，存储到context.User中
            app.UseAuthentication();
            // 判断当前访问Endpoint(Controller或Action)是否使用了[Authorize]以及配置角色或策略，然后校验Cookie或Token是否有效
            //app.UseAuthorization();

            // 启用MVC模式，声明了属性路由
            app.UseMvc();

            //app.UseAuthorization();
            // 配置跨域
            app.UseCors("EnableCORS");

            // 添加Swagger UI中间件
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            // 使用静态文件，这样可以房屋wwwroot文件目录
            app.UseStaticFiles();
            // 配置HTTP反射
            app.UseHttpsRedirection();

        }
    }
}
