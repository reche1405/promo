using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
namespace RecheApi.Data.Attributes.Models
{
    
[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class TableAttribute(string name, string? pluralName = null) : Attribute
{
    public string Name {get;set;} = name;
    public string PluralName {get;set;} = pluralName ?? name + "s";
}
}