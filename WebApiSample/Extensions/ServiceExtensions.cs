using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System.Security.Policy;
using System.Reflection;
using System.IO;

namespace WebApiSample.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 配置跨域 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }


        /// <summary>
        ///  配置IIS
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
            });
        }

        /// <summary>
        /// 配置Swagger中间件
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            // 注册Swagger生成器， 定义1个或多个Swagger文档
            services.AddSwaggerGen(c =>
            {
                // 编辑基本信息
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
                    Title = "My API", 
                    Version = "v1", 
                    Description = "Lingann Api",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Lingann Wang",
                        Email = "lingann.m@gmail.com",
                        Url = new Uri("http://sinzone.top")
                    }
                });
                // 配置xml注释
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGen();
        }
    }
}
