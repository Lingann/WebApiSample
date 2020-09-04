using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiSample.Helpers;

namespace Services.Authenciate
{
    public static class JwtMiddlewareExtension
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<JwtMiddleware>();
        }
    }
}
