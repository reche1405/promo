using System.ComponentModel.DataAnnotations.Schema;

namespace RecheApi.Models
{
    [Table("TextBlock")]
    public class TextBlock()
    {
        public int TextBlockId {get;set;}
        public string? Text {get;set;}
        public int? ColourId {get;set;}
    }
}