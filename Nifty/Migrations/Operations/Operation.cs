namespace RecheApi.Nifty.Migrations.Operations
{
    public abstract class Operation(int opId, int subOpId, string name)
    {
        private ModelCommand ModelCommand {get;set;} = new(opId, subOpId, name);
    }
}