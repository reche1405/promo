using Microsoft.Extensions.ObjectPool;

namespace RecheApi.Nifty.Migrations
{
    public class SchemaCommand 
    {
        public static readonly List<string> AvailableOps = [
            "CREATE TABLE IF NOT EXISTS",
            "DROP TABLE",
            "MODIFY TABLE"
        ];
        // This can be either a table name or a database name.
        private string? Name {get;set;} = null;
        private List<string> Columns = new();
        private List<string> Values = new(); 
        private string Operation = AvailableOps[0];


        public SchemaCommand SetName(string name)
        {
            this.Name = name; 
            return this;
        }
        public SchemaCommand SetOperationByIndex(int operationIndex)
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