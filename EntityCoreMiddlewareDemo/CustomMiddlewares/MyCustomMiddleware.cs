
namespace EntityCoreMiddlewareDemo.CustomMiddlewares
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Custom Middleware BEFORE\n");
            await next(context);
            await context.Response.WriteAsync("Custom Middleware AFTER\n");
        }
    }
    public static class CustomMiddlewareExtension
    {
        public static void MyMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
