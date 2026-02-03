using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Windows.Markup;

namespace RecheApi.Nifty.Database
{
    public class CommandSet<T> where T : new ()
    {
         public static readonly List<string> operations = new List<string> {
            "INSERT INTO ",
            "DELETE FROM "

        };

        private string _tableName = typeof(T).GetCustomAttribute<TableAttribute>()? .Name ?? typeof(T).Name;
        private string _operation = operations[0];

        private List<string> _columns = new ();
        private List<string> _values = new ();

        public CommandSet<T> Add(T model)
        {
            var props = typeof(T).GetProperties();
            for(int i = 0; i < props.Length; i++ )
            {
                string name = props[i].Name;
                var value = props[i].GetValue(model);
                if(value != null)
                {
                    _columns.Add(name);
                    _values.Add(value?.ToString() ?? "null");
                }
            }
            return this;
        }
        public string ToSQL()
        {
            string command = $"{_operation} {_tableName}";
            command += " ( " +  string.Join(" , ", _columns) + " ) ";
            command += "VALUES";
            command += " ( " +  string.Join(" , ", _values) + " ) ";
            
            return command;
        }

    }
}