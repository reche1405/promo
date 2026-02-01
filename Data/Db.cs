using Microsoft.Data.Sqlite; 
namespace RecheApi.Data
{
    public class Db
    {
        private readonly string _conString;

        public Db(string conString)
        {
            _conString = conString;

            using var con = new SqliteConnection(_conString);
            con.Open();
            var tableCommand = con.CreateCommand();
            tableCommand.CommandText = 
            @"
                CREATE TABLE IF NOT EXISTS Colour (
                    colourId INTEGER PRIMARY KEY AUTOINCREMENT,
                    title VARCHAR(255) NOT NULL,
                    red INTEGER DEFAULT 0,
                    green INTEGER DEFAULT 0,
                    blue INTEGER DEFAULT 0,
                    alpha FLOAT DEFAULT 1.0
                );

                INSERT INTO Colour (title, red, green, blue, alpha)
                VALUES ('Primary', 255, 160, 160, 1.0);

                CREATE TABLE IF NOT EXISTS Tag (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    text VARCHAR(255) NOT NULL,
                    colourId INTEGER,
                    FOREIGN KEY (colourId) REFERENCES Colour(colourId)
                );
            
                CREATE TABLE IF NOT EXISTS Media (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    path VARCHAR(255) UNIQUE NOT NULL,
                    title VARCHAR(255),
                    description VARCHAR(255)

                );

                CREATE TABLE IF NOT EXISTS TextBlock (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    text MEDIUMTEXT NOT NULL,
                    colourId INTEGER,
                    FOREIGN KEY (colourId) REFERENCES Colour(colourId)
                );


                CREATE TABLE IF NOT EXISTS Project (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    title VARCHAR(255) NOT NULL,
                    description MEDIUMTEXT NOT NULL,
                    startDate DATETIME DEFAULT CURRENT_DATE,
                    endDate DATETIME,
                    colourId INTEGER,


                    FOREIGN KEY (colourId) REFERENCES Colour(colourId)

                );

                INSERT INTO Project (title, description, colourId) 
                VALUES ('Hello World Projects', 'Lorem Ipsum dolor amet, some other stuff that i cant quite rememebr.', 1);

                CREATE TABLE IF NOT EXISTS ProjectTag (
                    projectId INTEGER NOT NULL,
                    tagId INTEGER NOT NULL,
                    
                    PRIMARY KEY (projectId, tagId),
                    FOREIGN KEY (projectId) REFERENCES Project(projectId),
                    FOREIGN KEY (tagId) REFERENCES Tag(tagId)
                );


                CREATE TABLE IF NOT EXISTS ProjectMedia (
                    projectId INTEGER NOT NULL,
                    mediaId INTEGER NOT NULL,
                    
                    PRIMARY KEY (projectId, mediaId),
                    FOREIGN KEY (projectId) REFERENCES Project(projectId),
                    FOREIGN KEY (mediaId) REFERENCES Media(mediaId)
                );
            ";
                    
            tableCommand.ExecuteNonQuery();
            con.Close();
        }
        public T QuerySingle<T>(string sql, Func<SqliteDataReader, T> map, params (string, object)[] parameters)
        {
            using var con = new SqliteConnection(_conString);
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            foreach(var (name, value) in parameters)
            {
                cmd.Parameters.AddWithValue(name,value);
            }
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? map(reader) : default!;

        }
        public List<T> Query<T>(string sql, Func<SqliteDataReader, T> map, params (string, object)[] parameters)
        {
            var results = new List<T>();
            using var con = new SqliteConnection(_conString);
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            foreach(var (name, value) in parameters)
            {
                cmd.Parameters.AddWithValue(name,value);
            }
            using var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                results.Add(map(reader));
            }

            return results;
        }

        public int Execute(string sql, params (string, object)[] parameters)
        {
             using var con = new SqliteConnection(_conString);
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            foreach(var (name, value) in parameters)
            {
                cmd.Parameters.AddWithValue(name,value);
            }
            return cmd.ExecuteNonQuery();
        }
    }
}