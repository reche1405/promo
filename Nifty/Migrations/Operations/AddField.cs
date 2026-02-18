using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace RecheApi.Nifty.Migrations.Operations
{
    public class AddField(string modelName, PropertyInfo prop, int opId = 3, int subOpId = 0) : Operation(opId, subOpId, modelName)
    {
        public string ModelName {get;set;} = modelName;
        public PropertyInfo Property {get;set;} = prop;

    }
}