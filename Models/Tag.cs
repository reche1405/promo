using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecheApi.Models
{
    [Table("Tag")]
    public class Tag : Model<Tag>
    {
        [Required]
        public int TagId {get;set;}
        public string? Text {get;set;}
        public int? ColourId {get; set; }
        public Tag() {}

    }
}