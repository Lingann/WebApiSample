# .Net Core 3 Web API 配置JWT

- [ASP.NET Core 2.2-带有示例API的基本身份验证教程](https://jasonwatmore.com/post/2018/09/08/aspnet-core-21-basic-authentication-tutorial-with-example-api)
- [ASP.NET Core 3.1-带有示例API的JWT身份验证教程](https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api#authorize-attribute-cs)
- [ASP.NET Core JWT 认证](https://beckjin.com/2019/03/24/aspnet-jwt/)
## JWT介绍
JWT(JSON Web Token)是一种开放标准，它以JSON对象的方式在各方之间安全的传输信息。通俗的说，就是通过数字签名算法生产一个字符串，然后在网络请求的中被携带到服务端进行身份认证，功能上来说和SessionId认证方式很像，

## JWT与 SessionId认证对比

SessionId认证方式一般做法是用户登录成功后，服务端生成一个SessionId,然后将SessionId和用户关系进行存储(内存、Redis、数据库等)，之后将SessionId写入Cookie(一般是主域名下，方便单点登录)或返回给调用放，后续的所有请求都携带这个SessionId到服务端进行身份认证。

而JWT最大区别是登录是状态不在服务端进行存储，而是通过密钥生成一个具有有效时间的Token返回给前端，Token中包含类似用户Id等信息，且是不允许被篡改的，之后的请求将Token携带到服务端进行认证，认证通过后可解析Token拿到用户标识进行后续操作。
