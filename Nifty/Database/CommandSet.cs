using RecheApi.Nifty.Attributes.Models;
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
                var attrs = props[i].GetCustomAttribute<ColumnAttribute>();
                if(attrs is null || attrs.IsPrimaryKey)
                {
                    continue;
                }
                string name = props[i].Name;
                var value = props[i].GetValue(model);
                if (value is not null && value.GetType().Equals(typeof(string)))
                {
                    string _value = (string)value;
                    _value = _value.Replace("'", "''");
                    _value = "'" + _value + "'";
                    _columns.Add(name);
                    _values.Add(_value);
                }
               
                else if(value is not null)
                {
                    _columns.Add(name);
                    _values.Add(value?.ToString() ?? "NULL");
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
            Console.WriteLine(command);
            return command;
        }

    }
}