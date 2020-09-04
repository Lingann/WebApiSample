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
using WebApiSample.Extensions;
using WebApiSample.Helpers;
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
            // 启用MVC模式，声明了属性路由
            app.UseMvc();
            // 配置跨域
            app.UseCors("EnableCORS");
            // 配置JWT身份验证, 添加JWT中间件
            app.UseAuthentication();
            //app.UseMiddleware<JwtMiddleware>();
        }
    }
}
