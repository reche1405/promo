using System.ComponentModel.DataAnnotations.Schema;

namespace RecheApi.Nifty.Migrations.Operations
{
    public class AddModel(string modelName, Type modelType) : Operation
    {
        public string ModeleName {get;set;} = modelName;

        public Type ModelType {get;set;} = modelType;

    }
}