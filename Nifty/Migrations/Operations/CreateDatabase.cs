namespace RecheApi.Nifty.Migrations.Operations
{
    public class CreateDatabase(string databaseName = "data") : Operation(1, -1, databaseName)
    {
        public string DatabaseName {get;set;} = databaseName;   
    }
}