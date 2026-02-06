using RecheApi.Nifty.Database;

namespace RecheApi.Nifty.Migrations
{
    public class TableIdentifier
    {
        private Dictionary<string, object> Tables = new();
        private  List<(string Name, Int64 nCols)> Schema = new();
        private DbContext con = new();

        public void GetSchema()
        {
           Schema = con.QuerySchema();
           
        }
        public void GetTableData()
        {
            if(Schema.Count() < 1)
            {
                
                throw new Exception("No Schema Information available in the current context.");
            }
            foreach((string Name, Int64 nCols) in Schema)
            {
                // string TableMeta = con.GetTableMeta(Name)
                // 
            }
        }
    }
}