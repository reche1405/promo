
using System.Reflection;
using RecheApi.Serializers.Fields;
using RecheApi.Nifty.Attributes.Models;
using System.Text.Json;
using RecheApi.Nifty.Serializers.DataTransfer;
namespace RecheApi.Nifty.Serializers
{

    public abstract class Serializer
    {
        protected readonly List<SerializerField> Fields = [];
        public ValidatedData? ValidatedData {get;set;} = null;
       

    }

    public class ModelSerializer<T> : Serializer  where T : class, new()
    {
    
        protected ModelSerializer(RequestData? requestData = null)
        {
            MapFields(requestData);
        }
        

        private void MapFields(RequestData? reqData = null)
        {
            var props = typeof(T).GetProperties();
            var rData = reqData as RequestData;
            foreach (PropertyInfo prop in props)
            {
                var attrs = prop.GetCustomAttribute<ColumnAttribute>();
                if(attrs is null) continue;
                SerializerField field = new(prop.Name, prop, attrs.IsRequired, attrs.IsReadOnly);
                if (reqData is not null)
                {   try
                    {
                        
                        object value = reqData.GetValueIgnoreCase<object>(prop.Name);
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



        public static Dictionary<string, object> Serialize(T model)
        {
            ModelData mData = new();
            var props = model.GetType().GetProperties() ??
            throw new Exception("Unable to locate properties on the provided model - Nifty Serializer.");
            
            foreach(var prop in props)
            {
                var colAttrs = prop.GetCustomAttribute<ColumnAttribute>();
                if(colAttrs is null) continue;

                var value = prop.GetValue(model);
                if(value is null) continue;

                mData.SetValue(prop.Name, value); 
            }
            
           

            return mData.Data();
        }

        public static List<Dictionary<string, object>> Serialize(List<T> modelList)
        {
            List<Dictionary<string, object>> data = new();
            foreach(T m in modelList) {
                Dictionary<string, object> modelData = Serialize(m);
                data.Add(modelData);
            };
            return data;
        }


        public bool IsValid()
        {
            ValidatedData data = new();
            foreach(SerializerField field in Fields)
            {
                object requested = field.RequestedValue?? throw new Exception("There is no requested data for this fiel");
                Type typeNeeded = field.ModelProperty.GetType();
                var converted =  requested; 
                data.SetValue(field.FieldName, field.RequestedValue);
            }

            ValidatedData = data;
            return true;
        }

        
    }
}