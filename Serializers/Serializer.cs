
using System.Reflection;
using RecheApi.Serializers.Fields;
using RecheApi.Data.Attributes.Models;
namespace RecheApi.Serializers
{

    public class ModelSerializer<T> where T : class
    {
        private readonly List<SerializerField> Fields = [];
        protected ModelSerializer()
        {
            MapFields();
        }

        private void MapFields()
        {
            var props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                var attrs = prop.GetCustomAttribute<ColumnAttribute>();
                if(attrs is null) continue;
                SerializerField field = new(prop.Name, prop, attrs.IsRequired, attrs.IsReadOnly);

                Fields.Add(field);
            }

        }
    }
}