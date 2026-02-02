using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.Data.Sqlite;

namespace RecheApi.Data
{

    public class Manager<T> where T : new ()
    {
          
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

    };
}