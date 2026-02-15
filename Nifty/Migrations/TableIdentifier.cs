using RecheApi.Nifty.Database;

namespace RecheApi.Nifty.Migrations
{
    public class TableIdentifier
    {
        private Dictionary<string, Dictionary<string, object>> Tables = new();
        private  List<(string Name, Int64 nCols)> Schema = new();
        private DbContext con = new();
        private bool SchemaIndexed = false;

        public void GetSchema()
        {
           Schema = con.QuerySchema();
           SchemaIndexed = true;
        }
        public void QueryTableMeta()
        {
            if(!SchemaIndexed)
            { 
                GetSchema();
            }if (Schema.Count() < 1)
            {
                throw new Exception("No Schema Information available in the current context.");
            }
        
            foreach((string Name, Int64 nCols) in Schema)
            {
                Dictionary<string, object> TableMeta = con.QueryTableMeta(Name);
                Tables.Add(Name, TableMeta);
            }
        }
        public Dictionary<string, Dictionary<string, object>> GetTables()
        {
            if (Tables.Count == 0)
            {
                QueryTableMeta();
            }
            return Tables;
        }
    }
}