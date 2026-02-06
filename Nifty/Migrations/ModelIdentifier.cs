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
    }
}