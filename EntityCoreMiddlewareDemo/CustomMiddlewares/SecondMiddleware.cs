using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EntityCoreMiddlewareDemo.CustomMiddlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SecondMiddleware
    {
        private readonly RequestDelegate _next;

        public SecondMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync("Second Custom Middleware BEFORE\n");
            await _next(httpContext);
            await httpContext.Response.WriteAsync("Second Custom Middleware AFTER\n");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SecondMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecondMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecondMiddleware>();
        }
    }
}
