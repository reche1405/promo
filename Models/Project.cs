

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecheApi.Data.Attributes;

namespace RecheApi.Models
{
    [Table("Project")]
    public class Project() : Model<Project>
    {
        
        public int ProjectId { get; set; }
        public string? Title { get; set; }

        public string? Description {get; set; }
        public string? StartDate { get; set; }
        public string? EndDate {get; set; }


    }
}