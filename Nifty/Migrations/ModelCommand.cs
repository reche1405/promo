using System.ComponentModel;
using Microsoft.Extensions.ObjectPool;

namespace RecheApi.Nifty.Migrations
{
    public class ModelCommand(int opId = 1, int subOpId = -1, string? name = null )
    {
        public static readonly List<string> AvailableOps = [
            "CREATE DATABASE",
            "CREATE TABLE IF NOT EXISTS",
            "DROP TABLE",
            "ALTER TABLE",

        ];
        public static readonly List<string> SubOps = [
            "ADD",
            "ALTER COLUMN",
            "DROP COLUMN",
        ];
        public static Dictionary<string, string> Attributes = new() {
            {"primaryKey", "PRIMARY KEY"},
            {"autoIncrement", "AUTOINCREMENT"},
            {"foreignKey", "FOREIGN KEY"}, 
            {"nullable", "NOT NULL"},               // This needs to be looked at as nullable implies NULL rather than NOT NULL.


        };
        // This can be either a table name or a database name.
        private string? Name {get;set;} = name;
        private List<string> Columns {get;set;} =  new();
        private List<string> Values {get;set;} = new(); 
        private string Operation {get;set;} =  AvailableOps[opId];
        private string? SubOperation {get;set;} = subOpId >= 0 ? SubOps[subOpId] : null;


        public ModelCommand SetName(string name)
        {
            this.Name = name; 
            return this;
        }
        public ModelCommand SetOperationByIndex(int operationIndex)
        {
            try
            {
                this.Operation = AvailableOps[operationIndex];
                return this;
            } catch (IndexOutOfRangeException)
            {
                string errMsg = $"There is no operation for index {operationIndex}\n";
                errMsg += "The options are as follows: \n"; 
                for(int i = 0; i < AvailableOps.Count; i++)
                {
                    errMsg += $"{i}: {AvailableOps[i]} \n";
                }

                throw new IndexOutOfRangeException(errMsg);
            }
        }


    }
}