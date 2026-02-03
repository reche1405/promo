
namespace RecheApi.Serializers
{
public class Data()
    {
        private Dictionary<string, object> _values = new();
        public void SetValue(string key, object value) => _values[key] = value;
        public T GetValue<T>(string key) => (T)_values[key];
    }
    
}