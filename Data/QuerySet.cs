using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

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

        public QuerySet<T> GetById(int id)
        {
            
            _operation = operations[0];
            _whereClauses.Add($"{_tableName}Id = {id}");
            return this;
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

    
    }
}