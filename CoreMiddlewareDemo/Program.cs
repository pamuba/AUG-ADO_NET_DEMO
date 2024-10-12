using CoreMiddlewareDemo.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Use( async (HttpContext context, RequestDelegate next) => {
    await context.Response.WriteAsync("Hello World\n");
    await next(context);
});

//app.UseMiddleware<MyCustomMiddleware>();
app.UseMyCustomMiddleware();
app.UseMyMiddleware();

app.Use(async (HttpContext context, RequestDelegate next) => {
    await context.Response.WriteAsync("Hello World Second\n");
    await next(context);
});


app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("Hello World Third\n");
});


app.Run();
