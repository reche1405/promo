using System.Data;
using Microsoft.Data.Sqlite; 
using RecheApi.Nifty.Application;
namespace RecheApi.Nifty.Database
{
    public class DbContext
    {
        private readonly string _conString = Config.ConnectionString;

        public DbContext()
        {

            // using var con = new SqliteConnection(_conString);
            //con.Open();
            //var tableCommand = con.CreateCommand();
            // tableCommand.CommandText = 
            /* @"
                CREATE TABLE IF NOT EXISTS Colour (
                    ColourId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title VARCHAR(255) NOT NULL,
                    Red INTEGER DEFAULT 0,
                    Green INTEGER DEFAULT 0,
                    Blue INTEGER DEFAULT 0,
                    Alpha FLOAT DEFAULT 1.0
                );


                CREATE TABLE IF NOT EXISTS Tag (
                    TagId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Text VARCHAR(255) NOT NULL,
                    ColourId INTEGER,
                    FOREIGN KEY (ColourId) REFERENCES Colour(ColourId)
                );
            
                CREATE TABLE IF NOT EXISTS Media (
                    MediaId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Path VARCHAR(255) UNIQUE NOT NULL,
                    Title VARCHAR(255),
                    Description VARCHAR(255)

                );

                CREATE TABLE IF NOT EXISTS TextBlock (
                    TextBlockId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Text MEDIUMTEXT NOT NULL,
                    ColourId INTEGER,
                    FOREIGN KEY (ColourId) REFERENCES Colour(ColourId)
                );


                CREATE TABLE IF NOT EXISTS Project (
                    ProjectId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title VARCHAR(255) NOT NULL,
                    Description MEDIUMTEXT NOT NULL,
                    StartDate DATETIME DEFAULT DATE(),
                    EndDate DATETIME,
                    ColourId INTEGER,


                    FOREIGN KEY (ColourId) REFERENCES Colour(ColourId)

                );

                CREATE TABLE IF NOT EXISTS ProjectTag (
                    ProjectId INTEGER NOT NULL,
                    TagId INTEGER NOT NULL,
                    
                    PRIMARY KEY (ProjectId, TagId),
                    FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
                    FOREIGN KEY (TagId) REFERENCES Tag(TagId)
                );


                CREATE TABLE IF NOT EXISTS ProjectMedia (
                    ProjectId INTEGER NOT NULL,
                    MediaId INTEGER NOT NULL,
                    
                    PRIMARY KEY (ProjectId, MediaId),
                    FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
                    FOREIGN KEY (MediaId) REFERENCES Media(MediaId)
                );
            ";
                */     
            // tableCommand.ExecuteNonQuery();
            // con.Close();
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

        public bool Add<T>(string sql, params (string, object)[] parameters)
        {
            return Execute(sql, parameters) == 1;
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

        public List<(string Name , Int64 nCols)> QuerySchema()
        {
            List<(string Name, Int64 nCols)> tables = new();
            string sql = "PRAGMA TABLE_LIST";
            using var con = new SqliteConnection(_conString);
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                IDataRecord rowData =  reader;
                string? tableName = rowData[1].ToString();
                if( tableName is null ||  tableName.Contains("sqlite")) 
                    continue;
                tables.Add((rowData[1].ToString() ?? "", (Int64)rowData[3]));
                Console.WriteLine(rowData[1].ToString());
            }
            return tables;
        }

        public Dictionary<string, object>QueryTableMeta(string TableName)
        {
            Dictionary<string, object> dict = new();
            string sql = $"PRAGMA table_xinfo({TableName})";
             using var con = new SqliteConnection(_conString);
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                IDataRecord record = reader;

                // The name of the table column (Or the row in this reader query.)
                string colName = record[1].ToString() ?? "";
                Console.WriteLine($"The column name is: {colName}");

                // The data type stored in the database
                string colType = record[2].ToString() ?? "";
                Console.WriteLine($"The column type is: {colType}");

                bool colNullable = !(bool)record[3];

                bool pk = (bool)record[4];
                dict.Add(colName, new{ colType, colNullable, pk});

                
            }
            return dict;
        }

    }
}