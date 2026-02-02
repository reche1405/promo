using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.Data.Sqlite;

namespace RecheApi.Data
{

    public class Manager<T> where T : new ()
    {
          
        private readonly Db _db;
        private readonly string _table = typeof(T).GetCustomAttribute<TableAttribute>() ? .Name ??
            throw new Exception("The Model is missing a class level [Table] attribute.");

        public QuerySet<T> All(QuerySet<T>? _qb = null)
        {
            
            QuerySet<T> qb =  _qb ?? new();

            return qb.All();

        }

        public QuerySet<T> GetById(int Id, QuerySet<T>? _qb = null)
        {
            QuerySet<T> qb =  _qb ?? new();
            
            return qb.GetById(Id);
        }

        public QuerySet<T> Where(string clause, QuerySet<T>? _qb = null)
        {
            QuerySet<T> qb =  _qb ?? new();
            return qb.Where(clause);
            
        }


        private T MapToModel(SqliteDataReader reader)
        {
            T obj = new();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for(int i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                var prop = props.FirstOrDefault( p => 
                string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase ));
            }

            return obj;
        }

        public List<T> ToList(QuerySet<T> _qb)
        {
            List<T> list = new();
            // TODO: get the connection string from a global setting,
            // Connect to the db and call the query function
            // Similar to the query single in to model
        }
    }
}