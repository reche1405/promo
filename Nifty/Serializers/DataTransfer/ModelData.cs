namespace RecheApi.Nifty.Serializers.DataTransfer
{
    public class ModelData : BaseData
    {
        public ModelData() : base()
        {
            
        }
        public Dictionary<string, object> Data()
        {
            Dictionary<string, object> dict = new();
            var keys = _values.Keys;
            foreach(var key in keys)
            {
                dict.Add(key, GetValue<object>(key));
            }
            return dict;
        }
    }
}