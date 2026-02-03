using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using RecheApi.Nifty.Models;
using RecheApi.Nifty.Database;
namespace RecheApi.Nifty.Models
{

    public class Manager<T> where T : new ()
    {
          
        private readonly string _table = typeof(T).GetCustomAttribute<TableAttribute>() ? .Name ??
            throw new Exception("The Model is missing a class level [Table] attribute.");

        private List<CommandSet<T>> sessionCommands = new();

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

        public void Create(T model)
        {
            CommandSet<T> cs = new();
            CommandSet<T> addCommand = cs.Add(model);
            sessionCommands.Add(addCommand);

        }

        public void Save()
        {
            if (sessionCommands.Count == 0)
            {
                
                Console.WriteLine("There were no SQL commands to execute.");
                return;
            }

            Db _db = new ();
            int modifiedRows;   // Inserted, Edited or deleted rows.
            int testCounter = 0;
            while(sessionCommands.Count != 0)
            {
                CommandSet<T> set = sessionCommands.First();
                modifiedRows = _db.Execute(set.ToSQL());
                Console.WriteLine($"Modified Rows {modifiedRows}");
                sessionCommands.Remove(set);
                testCounter++;
            }
            Console.WriteLine($"Final count of commands: {testCounter}");
        }

    };
}