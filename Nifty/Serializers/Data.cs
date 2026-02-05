
namespace RecheApi.Nifty.Serializers
{
public class Data()
    {
        private Dictionary<string, object> _values = new();
        public void SetValue(string key, object value) => _values[key] = value;
        public T GetValue<T>(string key) => (T)_values[key];

        public override string ToString()
        {
            string repr = "{ ";
            foreach((string key, object value ) in _values)
            {
                repr += $"\n t {key} : {value}";
            }
            repr += "\n}";
            return repr;
        }

        // Returns the string representation of a case insensitive lookup
        // Returns an empty string if no value was found.
        public string ContainsLike(string written)
        {
            string containsLike = "";
            var keys = _values.Keys;
            foreach (var key in keys)
            {
                int compare = string.Compare(written, key, StringComparison.OrdinalIgnoreCase);
                if (compare == 0)
                {
                    containsLike = key;
                    break;   
                }
            }

            return containsLike;
        }

        public T GetValueIgnoreCase<T>(string writtenKey)
        {
            string lookupKey = ContainsLike(writtenKey);
            if(string.IsNullOrEmpty(lookupKey)) 
                throw new KeyNotFoundException($"There is nothing similar to {writtenKey} in the Request Data dictionary.");
            return GetValue<T>(lookupKey);
        }
    }


    
}