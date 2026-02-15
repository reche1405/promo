using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace RecheApi.Nifty.Migrations.Operations
{
    public class AddField(string modelName, PropertyInfo prop) : Operation
    {
        public string ModeleName {get;set;} = modelName;
        public PropertyInfo Property {get;set;} = prop;

    }
}