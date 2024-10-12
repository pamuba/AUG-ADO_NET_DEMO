
namespace CoreMiddlewareDemo.CustomMiddlewares
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello From Custom Middleware BEFROE\n");
            await next(context);
            await context.Response.WriteAsync("Hello From Custom Middleware AFTER\n");
        }
    }
    public static class MyCustomMiddlewareExtension {
        public static IApplicationBuilder UseMyCustomMiddleware
            (this IApplicationBuilder app)
        { 
            return app.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
