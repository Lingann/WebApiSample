# Swagger 配置

地址: [Swagger使用](https://code-maze.com/swagger-ui-asp-net-core-web-api/)



## 一、为什么使用Swagger

单纯的项目接口在前后端开发人员使用时特别不舒服的，所以要推荐一个，即方面又美观的接口文档说明框架——Swagger

## 二、配置Swagger服务

### 2.1 引入 Nuget包

引入swagger插件方法有两个：

1. 去swagger或github上下载[源码](https://github.com/domaindrivendev/Swashbuckle.AspNetCore), 然后将源码引入自己的项目

2. 直接利用NuGet包添加程序集应用

   >	* 在包管理器中下载安装 Search "Swashbuckle.AspNetCore" --> Install
		* 在解决方案资源管理器 > 搜索安装 `Swashbuckle.AspNetCore`包

