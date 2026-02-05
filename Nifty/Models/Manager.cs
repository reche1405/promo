
using System.Reflection;
using RecheApi.Nifty.Models;
using RecheApi.Nifty.Database;
using RecheApi.Nifty.Attributes.Models;
using RecheApi.Nifty.Attributes.Serializers;
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

        public T Create(Nifty.Serializers.Data data) 
        {
            T model = new();

            var props = model.GetType().GetProperties() ?? throw new Exception("This model has no properties");
            foreach(var prop in props)
            {
                var colAttrs = prop.GetCustomAttribute<ColumnAttribute>();
                if (colAttrs is null )
                {
                    continue;
                }
                if (colAttrs.IsPrimaryKey)
                {
                    prop.SetValue(model, null);
                    continue;
                }
                string lookupName = prop.Name;
                try
                {
                    object value = data.GetValueIgnoreCase<object>(lookupName);
                    prop.SetValue(model, value);
                } catch(KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;   
                }
            }

            CommandSet<T> cs = new();
            CommandSet<T> addCommand = cs.Add(model);
            sessionCommands.Add(addCommand);

            return model;

        }

        public void Save()
        {
            if (sessionCommands.Count == 0)
            {
                
                Console.WriteLine("There were no SQL commands to execute.");
                return;
            }

            DbContext _db = new ();
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