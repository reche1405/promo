using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace RecheApi.Nifty.Migrations.Operations
{
    public class AlterField(string modelName, PropertyInfo prop, int opId = 3, int subOpId = 1) : Operation(opId, subOpId, modelName)
    {
        public string ModeleName {get;set;} = modelName;
        public PropertyInfo Property {get;set;} = prop;

    }
}