
using RecheApi.Nifty.Attributes.Models;
using RecheApi.Nifty.Models;
namespace RecheApi.Models
{
    [Table(name: "TextBlock")]
    public class TextBlock() : Model<TextBlock>
    {
        [Column(primaryKey: true, readOnly: true)]
        public int TextBlockId {get;set;}
       
        [Column(required: true)]
        public string? Text {get;set;}
        [Column(foreignKey:"Colour")]
        public int? ColourId {get;set;}
    }
}