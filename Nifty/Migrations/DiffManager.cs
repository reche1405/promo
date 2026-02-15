using System.Collections.ObjectModel;

namespace RecheApi.Nifty.Migrations
{
    
    public class DiffManager()
    {
        public readonly Dictionary<Type, string> ColumnTypes = new Dictionary<Type, string>()
        {
            {typeof(int), "INTEGER"},
            {typeof(string), "VARCHAR , MEDIUMTEXT"},
            {typeof(float), "FLOAT"},
            {typeof(double), "DOUBLE"},
            {typeof(bool), "INTEGER"},
            {typeof(DateTime), "DATETIME"},

        };     
        private readonly TableIdentifier tIder = new();
        private Dictionary<string, List<string>> Actions {get;set;} = new() {
            {"Create", []},
            {"Remove", []},
            {"Modify", []},
        };



    }
}