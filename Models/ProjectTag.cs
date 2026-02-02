using System.ComponentModel.DataAnnotations.Schema;

namespace RecheApi.Models
{
    [Table("ProjectTag")]
    public class ProjectTag()
    {
        public int ProjectId {get;set;}
        public int TagId {get;set;}

    }
}