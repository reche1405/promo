namespace RecheApi.Nifty.Migrations.Operations
{
    public class DeleteModel(string modelName, int opId = 2) : Operation(opId, -1, modelName)
    {
        public string ModeleName {get;set;} = modelName;
    }
}