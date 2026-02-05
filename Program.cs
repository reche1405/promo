
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using RecheApi.Models;
using RecheApi.Nifty.Serializers;
using RecheApi.Serializers;
using RecheApi.Nifty.Serializers.DataTransfer;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use( async (context, next) =>
{
    string method = context.Request.Method;
    bool isNonQuery = false;
    string[] parsableMethods = ["POST", "PUT", "PATCH"];
    
    foreach(string m in parsableMethods)
    {
        if(string.Compare(method, m , StringComparison.OrdinalIgnoreCase) == 0)
        {
            isNonQuery = true;
            break;
        }
    }

    if (!isNonQuery) {
        
        await next(context);
        return;
    }

    RequestData data = new();
    using var sr = new StreamReader(context.Request.Body);
    var body = await sr.ReadToEndAsync();
    if(string.IsNullOrEmpty(body))
    {
        await next(context);
        return;
    }

    var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(body);
     if(dict is null)
    {
        await next(context);
        return;
        
    }

    foreach((string key, object value ) in dict)
    {
        data.SetValue(key, value);
    }
    context.Items["Data"] = data;
     await next(context);
    return;
});

app.MapGet("/", () =>
{
    
    var projects = Project.Objects.All().Where("ProjectId > 2").ToList();
    var colours = Colour.Objects.All().ToList();
    var projectData = ProjectSerializer.Serialize(projects);
    return new
    {
         projectData
    };
});
app.MapPost("/", (context) =>
{
    var data = context.Items["Data"] as RequestData;
    if (data is null)
    {
        context.Response.StatusCode = 500;
        return Task.CompletedTask;
    }
    Console.WriteLine(data.ToString());

    ProjectSerializer serializer = new(data);
    // If (!serializer.IsValid())  { context.Response.StatusCode = 400; return Task.CompletedTask};
    if (!serializer.IsValid())
    {
        context.Response.StatusCode = 400;
        return Task.CompletedTask;
    }
    ValidatedData validated = serializer.ValidatedData ?? throw new Exception("There was no validated data");
    Console.WriteLine(validated.ToString());
    Project project = Project.Objects.Create(validated);
    Project.Objects.Save();
    context.Response.StatusCode = 201;
    return Task.CompletedTask;
} );

app.Run();
