namespace RecheApi.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    class KeyAtribute(string value) : Attribute
    {
        public string Value = value;
    }
}