using System.ComponentModel.DataAnnotations.Schema;

namespace RecheApi.Nifty.Migrations.Operations
{
    public class AddField(string modelName, string fieldName, Type fieldType, ColumnAttribute attrs) : Operation
    {
        public string ModeleName {get;set;} = modelName;
        public string FieldName {get;set;} = fieldName;

        public Type FieldType {get;set;} = fieldType;

        public ColumnAttribute Attrs {get;set;} = attrs;
    }
}