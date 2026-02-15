namespace RecheApi.Nifty.Migrations.Operations
{
    public class DeleteModel(string modelName) : Operation
    {
        public string ModeleName {get;set;} = modelName;
    }
}