using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace RecheApi.Nifty.Migrations.Operations
{
    public class AlterField(string modelName, PropertyInfo prop) : Operation
    {
        public string ModeleName {get;set;} = modelName;
        public PropertyInfo Property {get;set;} = prop;

    }
}