
using RecheApi.Data.Attributes.Models;

namespace RecheApi.Models
{
    [Table(name: "TextBlock")]
    public class TextBlock()
    {
        [PrimaryKey]
        public int TextBlockId {get;set;}
       
        [Required]
        public string? Text {get;set;}
        public int? ColourId {get;set;}
    }
}