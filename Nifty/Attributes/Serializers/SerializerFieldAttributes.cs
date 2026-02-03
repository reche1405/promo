namespace RecheApi.Data.Attributes. Serializers
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    class SerializeAttribute(string name, bool required, string source) : Attribute
    {
        public string Name = name;
        public bool Required = required;
        public string Source = source;
    }
}