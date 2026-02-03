using System.Reflection;
using RecheApi.Models;

namespace RecheApi.Serializers.Fields
{
    public class SerializerField (string fieldName, PropertyInfo prop, bool isRequired = false, bool isReadOnly = false)
    {
        public string FieldName {get;set;} = fieldName;
        public PropertyInfo ModelProperty {get;set;} = prop;
        public bool IsRequired {get;set;} = isRequired;

        public bool IsReadOnly {get;set;} = isReadOnly;

        public object? RequestedValue {get;set;}

        public object? GetValue(object instance)
        {
            return ModelProperty.GetValue(instance);
        }
    }

}