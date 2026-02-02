using System.ComponentModel.DataAnnotations.Schema;

namespace RecheApi.Models
{
    [Table("Media")]
    public class Media()
    {
        public int MediaId {get;set;}
        public string? Path {get;set;}
        public string? Title {get;set;}
        public string? Description {get;set;}
        
        
    }
}