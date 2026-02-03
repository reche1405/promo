using RecheApi.Nifty.Attributes.Models;
using RecheApi.Nifty.Models;
namespace RecheApi.Models
{
    [Table("Media")]
    public class Media() : Model<Media>
    {
        [Column(primaryKey: true, autoIncrement: true)]
        public int MediaId {get;set;}

        [Column(required:true)]
        public string? Path {get;set;}

        [Column(required:true)]
        public string? Title {get;set;}
        [Column]
        public string? Description {get;set;}
        
        
    }
}