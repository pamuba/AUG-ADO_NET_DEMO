using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CoreMiddlewareDemo.CustomMiddlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Query.ContainsKey("firstname") &&
                httpContext.Request.Query.ContainsKey("lastname")) {
                string fullName = httpContext.Request.Query["firstname"] + " " +
                    httpContext.Request.Query["lastname"];
                await httpContext.Response.WriteAsync(fullName+"\n");
            }

            await _next(httpContext);
            await httpContext.Response.WriteAsync("FirstName or LastName AFTER\n");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}
