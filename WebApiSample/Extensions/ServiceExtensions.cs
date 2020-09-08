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
using Microsoft.Extensions.Configuration;
using Entities;
using Repository;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Services.Authenciate;
using Contracts.Services;

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

        /// <summary>
        /// 配置连接数据库
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSqliteContext(this IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>();
        }

        /// <summary>
        /// 加入存储库
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }


        /// <summary>
        /// 配置日志服务
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        /// <summary>
        /// 配置JWT服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
       public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var authSettings = config.GetSection("AuthSettings").Get<AuthSettings>();

            services.Configure<AuthSettings>(config.GetSection("AuthSettings"));

            services.AddScoped<IAuthService, AuthService>();

            services.AddAuthentication(opt =>
           {
               opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           }).AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;

               options.SaveToken = true;

               options.TokenValidationParameters = new TokenValidationParameters
               {
                    // The issuer is the actual server that created the token (ValidateIssuer=true)
                    ValidateIssuer = false,

                    // 校验Issure
                    ValidIssuer = authSettings.Issuer,

                    // The receiver of the token is a valid recipient(ValidateAudience = true)
                    ValidateAudience = false,

                    // 校验Audience
                    ValidAudience = authSettings.Audience,

                    // The token has not expired (ValidateLifetime=true)
                    ValidateLifetime = true,

                    // The signing key is valid and is trusted by the server (ValidateIssuerSigningKey=true)
                    ValidateIssuerSigningKey = true,

/*                   ValidIssuer = "https://localhost:5001",
                   ValidAudience = "https://localhost:5001",*/
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Secret))
               };
           });
        }
    }
}
