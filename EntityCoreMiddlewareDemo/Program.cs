using EntityCoreMiddlewareDemo.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

//Order of the middleware in .netcore
//UseRouting
//UseEndPoints

app.Use(async (HttpContext context, RequestDelegate next) => {
    await context.Response.WriteAsync("HELLO WORLD 1st\n");
    await next(context);
    await context.Response.WriteAsync("HELLO WORLD 1st AFTER\n");
});

//app.UseMiddleware<MyCustomMiddleware>();
//app.UseRouting()
app.MyMiddleware();

app.UseWhen(
        context => context.Request.Query.ContainsKey("username"),
        app => {
            app.Use(
                 async (context, next) => {
                     await context.Response.WriteAsync("The Key Exists");
                     await next(context);
                 });
        });

app.UseSecondMiddleware();

app.Use(async (HttpContext context, RequestDelegate next) => {
    await context.Response.WriteAsync("HELLO WORLD 2nd\n");
    await next(context);
    await context.Response.WriteAsync("HELLO WORLD 2st AFTER\n");
});

app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("HELLO WORLD 3rd\n");
});

//runs the web app finally
app.Run();
