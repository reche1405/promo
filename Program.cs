using Microsoft.Data.Sqlite;
using RecheApi.Data;
using RecheApi.Models;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    
    var projects = Project.Objects.All().Where("ProjectId > 2").ToList();
    var colours = Colour.Objects.All().ToList();
    return new
    {
         projects,
         colours 
    };
});

app.Run();
