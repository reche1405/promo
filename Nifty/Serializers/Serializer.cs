
using System.Reflection;
using RecheApi.Serializers.Fields;
using RecheApi.Nifty.Attributes.Models;
namespace RecheApi.Serializers
{

    public abstract class Serializer
    {
        protected readonly List<SerializerField> Fields = [];
       

    }

    public class ModelSerializer<T> : Serializer  where T : class 
    {
    
        protected ModelSerializer(Data? requestData = null)
        {
            MapFields(requestData);
        }

        private void MapFields(Data? data = null)
        {
            var props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                var attrs = prop.GetCustomAttribute<ColumnAttribute>();
                if(attrs is null) continue;
                SerializerField field = new(prop.Name, prop, attrs.IsRequired, attrs.IsReadOnly);
                if (data is not null)
                {
                    object value = data.GetValue<object>(prop.Name);
                    field.RequestedValue = value;
                }
                Fields.Add(field);
            }

        }

      

        public Data Serialize(T model)
        {
            Data data = new();
            Fields.ForEach(f =>
            {
                data.SetValue(f.FieldName, f.GetValue(model) ?? ""); 
            });

            return data;
        }

        
    }
}