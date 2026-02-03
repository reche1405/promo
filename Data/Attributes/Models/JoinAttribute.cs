namespace RecheApi.Data.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class |System.AttributeTargets.Struct)]
    public class JoinAttribute(string leftName, string rightName) : Attribute {
        public string LeftTableName { get; set; } = leftName;
        public string RightTableName { get; set; } = rightName;
    }
}