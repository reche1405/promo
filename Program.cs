using Microsoft.Data.Sqlite;
using RecheApi.Data;
using RecheApi.Models;
using RecheApi.Models.Repos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var conStr = "Data Source = reche.db";

var db = new Db(conStr);

var projectRepo = new BaseRepo<Project>(db, "ProjectId");

app.MapGet("/", () =>
{
    var projects =  projectRepo.GetAll();

    return new
    {
         projects 
    };
});

app.Run();
