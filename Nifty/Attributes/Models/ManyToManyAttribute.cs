namespace RecheApi.Nifty.Attributes.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ManyToManyAttribute(string otherModel, 
    string? relatedName = null, string? reverseName = null) : Attribute
    {
        public string OtherModel {get;set;} = otherModel;
        public string? RelatedName {get;set;} = relatedName;
        public string? ReverseName {get; set; } = reverseName;
    }
}