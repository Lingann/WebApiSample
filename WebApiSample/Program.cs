using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApiSample.Models;
namespace WebApiSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)  // ASP.NET Core使用它来配置应用程序配置，日志记录和依赖项注入容器
                .UseStartup<Startup>();  // .NET Core 必须具有Startup类，在其中我们配置了应用程序所需的嵌入式或自定义服务
    }
}
