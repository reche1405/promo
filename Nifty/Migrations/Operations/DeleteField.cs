using System.ComponentModel.DataAnnotations.Schema;

namespace RecheApi.Nifty.Migrations.Operations
{
    public class DeleteField(string modelName, string fieldName) : Operation
    {
        public string ModeleName {get;set;} = modelName;
        public string FieldName {get;set;} = fieldName;

    }
}