using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.Data.Sqlite;
using RecheApi.Data;
namespace RecheApi.Models.Repos
{
    public class BaseRepo<T>(Db db, string pk) where T :  new()
    {
        private readonly Db _db = db;
        private readonly string _pk = pk;

        private readonly string _table = typeof(T).GetCustomAttribute<TableAttribute>()?.Name ??
                throw new Exception("Missing [Table] attribute.");

        public IEnumerable<T> GetAll()
        {
            return _db.Query($"SELECT * FROM {_table}", MapToModel);
        }

        public T GetById(int id)
        {
            return _db.QuerySingle($"SELECT * FROM  {_table} WHERE {_pk} =  {id}", MapToModel);
        }

        public static T MapToModel(SqliteDataReader reader)
        {
            var obj = new T();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var column = reader.GetName(i);

                var prop = props.FirstOrDefault(p =>
                    string.Equals(p.Name, column, StringComparison.OrdinalIgnoreCase));

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

    }
}