using RecheApi.Nifty.Database;

namespace RecheApi.Nifty.Migrations
{
    public class TableIdentifier
    {
        private List<Dictionary<string, object>> Tables = new();
        private  List<(string Name, Int64 nCols)> Schema = new();
        private DbContext con = new();
        private bool SchemaIndexed = false;

        public void GetSchema()
        {
           Schema = con.QuerySchema();
           SchemaIndexed = true;
        }
        public void GetTableMeta()
        {
            if(!SchemaIndexed)
            { 
                GetSchema();
            } else if (Schema.Count() < 1)
            {
                throw new Exception("No Schema Information available in the current context.");
            }
        
            foreach((string Name, Int64 nCols) in Schema)
            {
                Dictionary<string, object> TableMeta = con.QueryTableMeta(Name);
                Tables.Add(TableMeta);
            }
        }
    }
}