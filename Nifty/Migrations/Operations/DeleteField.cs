using System.ComponentModel.DataAnnotations.Schema;

namespace RecheApi.Nifty.Migrations.Operations
{
    public class DeleteField(string modelName, string fieldName, int opId = 4, int subOpId = 3) : Operation(opId, subOpId, modelName)
    {
        public string ModeleName {get;set;} = modelName;
        public string FieldName {get;set;} = fieldName;

    }
}