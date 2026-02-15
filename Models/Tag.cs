using RecheApi.Nifty.Attributes.Models;
using RecheApi.Nifty.Models;

namespace RecheApi.Models
{
    [Table("Tag")]
    public class Tag() : Model<Tag>
    {
        [Column(primaryKey: true, autoIncrement: true)]
        public int TagId {get;set;}

        [Column(nullable: false)]
        public string? Text {get;set;}
        [Column]
        public int? ColourId {get; set; }

    }
}