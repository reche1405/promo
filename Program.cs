using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var con = "Data Source = reche.db";
using(var connection = new SqliteConnection(con))
{
    var tableCommand = connection.CreateCommand();
    tableCommand.CommandText = 
    @"
        CREATE TABLE IF NOT EXISTS Colour (
            id INTEGER PRIMARY KEY AUTO INCREMENT,
            title VARCHAR(255) NOT NULL,
            red INTEGER DEFAULT 0,
            green INTEGER DEFAULT 0,
            blue INTEGER DEFAULT 0,
            alpha FLOAT DEFAULT 1.0
        );

        CREATE TABLE IF NOT EXISTS Tag (
            id INTEGER PRIMARY KEY AUTO INCREMENT,
            text VARCHAR(255) NOT NULL,
            colourId INTEGER,
            FOREIGN KEY (colourId) REFERENCES Colour(id)
        );
    
        CREATE TABLE IF NOT EXISTS Media (
            id INTEGER PRIMARY KEY AUTO INCREMENT,
            path VARCHAR(255) UNIQUE NOT NULL,
            title VARCHAR(255)
            description VARCHAR(255)

        );

        CREATE TABLE IF NOT EXISTS TextBlock (
            id INTEGER PRIMARY KEY AUTO INCREMENT,

        );


        CREATE TABLE IF NOT EXISTS Project (
            id INTEGER PRIMARY KEY AUTO INCREMENT,
            title VARCHAR(255) NOT NULL,
            description MEDIUMTEXT NOT NULL,
            startDate DATETIME DEFAULT CURRENT_DATE,
            endDate DATETIME,
            colourId INTEGER,


            FOREIGN KEY (colourId) REFERENCES Colour(id)

        );

        CREATE TABLE IF NOT EXISTS ProjectTag (
            projectId INTEGER NOT NULL,
            tagId INTEGER NOT NULL,
            
            PRIMARY KEY (projectId, tagId),
            FOREIGNKEY (tagId) REFERENCES Tag(id)
        );


        CREATE TABLE IF NOT EXISTS ProjectMedia (
            projectId INTEGER NOT NULL,
            mediaId INTEGER NOT NULL,
            
            PRIMARY KEY (projectId, mediaId),
            FOREIGN KEY (projectId) REFERENCES Project(id),
            FOREIGN KEY (mediaId) REFERENCES Media(id)
        );
    ";
    tableCommand.ExecuteNonQuery();
}

app.MapGet("/", () => "Hello World!");

app.Run();
