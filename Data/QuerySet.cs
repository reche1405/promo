using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.Data.Sqlite;

namespace RecheApi.Data
{
    public class QuerySet<T> where T : new ()
    {
        public static readonly List<string> operations = new List<string> {
            "SELECT * FROM",
            "INSERT INTO TABLE",
            "CREATE NEW TABLE",

        };
        
        private string _tableName;

        private string _operation = operations[0];
        private List<string> _whereClauses = new();
        private string? _orderBy;

        private string _ordering = "ASC";
        private int? _limit;

        public QuerySet()
        {
            var titleAttr = typeof(T).GetCustomAttribute<TableAttribute>();

            _tableName =  titleAttr ? .Name ?? typeof(T).Name;
        }

        public QuerySet<T> All()
        {
            _operation = operations[0];
            return this;
        }

        public T GetById(int id, Db _db)
        {
            
            _operation = operations[0];
            _whereClauses.Add($"{_tableName}Id = {id}");
            return _db.QuerySingle(ToSQL(), MapToModel);
        }
        public QuerySet<T> Where(string clause)
        {
            _whereClauses.Add(clause);
            return this;
        }

        public QuerySet<T> Limit(int limit)
        {
            _limit = limit;
            return this;
        }

        public QuerySet<T> OrderBy(string columnName, string ordering = "ASC")
        {
            _orderBy = columnName;
            _ordering = ordering;
            return this;
        }

        public static T MapToModel(SqliteDataReader reader)
        {
            T obj = new();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for(int i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                var prop = props.FirstOrDefault( p => 
                string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase ));
                if (prop != null && !reader.IsDBNull(i))
                {
                    var value = reader.GetValue(i);

                    // Handles int, string, bool, DateTime, etc.
                    var converted = Convert.ChangeType(value, prop.PropertyType);
                    prop.SetValue(obj, converted);
                }
            }

            return obj;
        }

         public string ToSQL()
        {
            string sql = $"{_operation} {_tableName}";
            if (_whereClauses.Any()) 
                sql += $" WHERE " + string.Join(" AND ", _whereClauses);
            if (_orderBy != null ) 
                sql += $" ORDER BY {_orderBy}";
            if (_limit.HasValue) 
                sql += $" LIMIT {_limit}";
            return sql;
        }
        public List<T> ToList(Db _db)
        {
            return _db.Query(ToSQL(), MapToModel);
            // TODO: get the connection string from a global setting,
            // Connect to the db and call the query function
            // Similar to the query single in to model
            
        }

        public T First(Db _db)
        {
            _limit = 1;
            return _db.QuerySingle(ToSQL(), MapToModel);
        }

       
    }
}