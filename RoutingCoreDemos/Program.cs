using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//GetEndPoint()
app.Use(async (context, next) => {
    Microsoft.AspNetCore.Http.Endpoint? endPoint = context.GetEndpoint();
    if (endPoint != null) {
        await context.Response.WriteAsync($"EndPoint:{endPoint.ToString()}\n");
    }
    await next(context);
});

//Enable Routing
app.UseRouting();

//GetEndPoint()
app.Use(async (context, next) => {
    Microsoft.AspNetCore.Http.Endpoint? endPoint = context.GetEndpoint();
    if (endPoint != null)
    {
        await context.Response.WriteAsync($"EndPoint:{endPoint.DisplayName}\n");
    }
    await next(context);
});

#pragma warning disable ASP0014 // Suggest using top level route registrations


app.UseEndpoints(endpints =>
{
    //add our endpoints
    endpints.Map("map1", async (context) =>
    {
        await context.Response.WriteAsync("Route: /map1\n");
    });
});


app.UseEndpoints(endpints =>
{
    //add our endpoints
    endpints.Map("map2", async (context) =>
    {
        await context.Response.WriteAsync("Route: /map2\n");
    });
});

//files/{filename}.{extension}
//files/data.txt
//file/?filename=data.txt
app.UseEndpoints(endpints =>
{
    //add our endpoints
    endpints.Map("files/{filename}.{extension}", async (context) =>
    {
        string? filename = Convert.ToString(context.Request.RouteValues["filename"]);
        string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
        await context.Response.WriteAsync($"File Name in DB: {filename}.{extension}");
    });
});


app.UseEndpoints(endpints =>
{
    //add our endpoints
    endpints.Map("employee/profile/{EmployeeName:alpha:length(1,8)=John}", async (context) =>
    {
        string? EmployeeName = Convert.ToString(context.Request.RouteValues["EmployeeName"]);
        await context.Response.WriteAsync($"Employee Name in DB: {EmployeeName}");
    });
});



app.UseEndpoints(endpints =>
{
    //add our endpoints
    //endpints.Map("employee/dept/{EmployeeID:int:min(333):max(888)?}", async (context) =>
    endpints.Map("employee/dept/{EmployeeID:int:range(333,888)?}", async (context) =>
     {
        int? EmployeeID = Convert.ToInt32(context.Request.RouteValues["EmployeeID"]);
        await context.Response.WriteAsync($"Employee ID in DB: {EmployeeID}");
    });
});

app.Run(async context => {
    await context.Response.WriteAsync("No endpoints MATCHED\n");
});

#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Run();
