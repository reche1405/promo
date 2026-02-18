using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace RecheApi.Nifty.Migrations.Operations
{
    public class AddModel(string modelName, List<PropertyInfo> props, int opId = 1) : Operation(opId, -1, modelName)
    {
        public string ModelName {get;set;} = modelName;

        public List<PropertyInfo> Properties {get;set;} = props;

    }
}