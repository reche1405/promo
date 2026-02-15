using RecheApi.Models;

namespace RecheApi.Nifty.Attributes.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute
    (bool primaryKey = false, bool nullable = true, int maxLength = 0, 
    bool autoIncrement = false, string foreignKey = "", bool autoNowAdd = false,
    bool autoNow = false, bool readOnly = false, bool serialize = false 
    
     ) : Attribute
    {
        public bool IsPrimaryKey {get;set;} = primaryKey;

        public bool IsReadOnly {get;set;} = readOnly;
        public int? MaxLength {get;set;} = maxLength > 0 ? maxLength : null;
        public bool AutoIncrement {get; set;} = autoIncrement;
        public bool Nullable {get;set;} = primaryKey || foreignKey.Length > 0 ? false : nullable;
        public bool IsForeignKey {get;set;} = foreignKey.Length > 0;
        public string? ForeignKeyTableName {get;set;} = foreignKey.Length > 0 ? foreignKey : null;

        public bool AutoNowAdd {get;set;} = autoNowAdd;
        public bool AutoNow {get;set;} = autoNow;

        public bool Serialize {get;set;} = serialize;
        
    }

}