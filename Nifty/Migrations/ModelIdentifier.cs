using System.Reflection;
using RecheApi.Nifty.Attributes.Models;
using RecheApi.Nifty.Models;

namespace RecheApi.Nifty.Migrations
{
    public static class ModelIdentifier
    {
        private static readonly Type baseType = typeof(BaseModel);
        public static IEnumerable<Type> GetModelTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract);

        }
        public static List<Dictionary<string, object>> GetModels()
        {
            IEnumerable<Type> types = GetModelTypes();
            var data = new List<Dictionary<string, object>>();
            foreach(Type t in types)
            {
                Dictionary<string, object> dict = new();
                var props = t.GetProperties();
                // Loop through the properties and add their name and attributes.

                foreach(var prop in props)
                {
                    string prpName = prop.Name;
                    ColumnAttribute? attrs = prop.GetCustomAttribute<ColumnAttribute>();
                    if (attrs is null) continue;
                    // Need to get the type of the property, if it is nullable, and if it is a primary key.
                    string colType = prop.GetType().ToString();
                    bool colNullable = attrs.Nullable;
                    bool pk = attrs.IsPrimaryKey;

                    dict.Add(prpName, new {colType, colNullable, pk});
                }
                data.Add(dict);
            }

            return data;   
        }
    }
}