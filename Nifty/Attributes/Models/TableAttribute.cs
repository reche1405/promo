namespace RecheApi.Nifty.Attributes.Models
{
    
[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class TableAttribute(string name, string? pluralName = null) : Attribute
{
    public string Name {get;set;} = name;
    public string PluralName {get;set;} = pluralName ?? name + "s";
}
}