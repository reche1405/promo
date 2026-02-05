
using System.Reflection;
using RecheApi.Serializers.Fields;
using RecheApi.Nifty.Attributes.Models;
using System.Text.Json;
namespace RecheApi.Nifty.Serializers
{

    public abstract class Serializer
    {
        protected readonly List<SerializerField> Fields = [];
       

    }

    public class ModelSerializer<T> : Serializer  where T : class, new()
    {
    
        protected ModelSerializer(Data? requestData = null)
        {
            MapFields(requestData);
        }

        private void MapFields(Data? data = null)
        {
            var props = typeof(T).GetProperties();
            var _data = data as Data;
            foreach (PropertyInfo prop in props)
            {
                var attrs = prop.GetCustomAttribute<ColumnAttribute>();
                if(attrs is null) continue;
                SerializerField field = new(prop.Name, prop, attrs.IsRequired, attrs.IsReadOnly);
                if (data is not null)
                {   try
                    {
                        
                        object value = data.GetValueIgnoreCase<object>(prop.Name);
                        Type type = field.ModelProperty.GetType();
                        string parsed = value.ToString()?? "";
                        field.RequestedValue = parsed;
                        Console.WriteLine(field.RequestedValue);
                    } catch (KeyNotFoundException e)
                    {
                        string eMsg = $"Value {prop.Name} not found in Data request object: " + e.Message;
                        Console.WriteLine(eMsg);
                        continue;
                    }
                }
                Fields.Add(field);
            }

        }



        public static Data Serialize(T model)
        {
            Data data = new();
            var props = model.GetType().GetProperties() ??
            throw new Exception("Unable to locate properties on the provided model - Nifty Serializer.");
            
            foreach(var prop in props)
            {
                var colAttrs = prop.GetCustomAttribute<ColumnAttribute>();
                if(colAttrs is null) continue;

                var value = prop.GetValue(model);
                if(value is null) continue;

                data.SetValue(prop.Name, value); 
            }
            
           

            return data;
        }

        public Data ValidatedData()
        {
            Data data = new();
            foreach(SerializerField field in Fields)
            {
                object? requested = field.RequestedValue;
                Type typeNeeded = field.ModelProperty.GetType();
                var converted =  requested; 
                data.SetValue(field.FieldName, field.RequestedValue);
            }

            return data;
        }

        
    }
}