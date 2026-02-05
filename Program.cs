using RecheApi.Models;
using RecheApi.Serializers;
using RecheApi.Nifty.Serializers.DataTransfer;
using RecheApi.Nifty.Application;


//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

var AppFactory = new NiftyAppFactory();
AppFactory.CreateBuilder(args);
var nApp = AppFactory.Build();


nApp.MapGet("/", () =>
{
    
    var projects = Project.Objects.All().Where("ProjectId > 2").ToList();
    var colours = Colour.Objects.All().ToList();
    var projectData = ProjectSerializer.Serialize(projects);
    return new
    {
         projectData
    };
});
nApp.MapPost("/", (context) =>
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

nApp.Run();
