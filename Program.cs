using Microsoft.Data.Sqlite;
using RecheApi.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var conStr = "Data Source = reche.db";

var db = new Db(conStr);

app.MapGet("/", () =>
{
    return db.Query(
        "SELECT id, title, description FROM Project",
        entry => new {id = entry.GetInt32(0), title = entry.GetString(1), description = entry.GetString(2)}
    );
    
});

app.Run();
