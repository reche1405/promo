using RecheApi.Nifty.Attributes.Models;
using RecheApi.Nifty.Models;
namespace RecheApi.Models
{
    [Table("Project")]
    public class Project() : Model<Project>
    {
        
        [Column(primaryKey : true, autoIncrement: true)]
        public int ProjectId { get; set; }

        [Column(required: true)]
        public string? Title { get; set; }

        [Column(required: true)]
        public string? Description {get; set; }

        [Column(autoNowAdd: true)]
        public string? StartDate { get; set; }
        [Column]
        public string? EndDate {get; set; }

        [ManyToMany(otherModel:"Tag", relatedName: "Tags", reverseName: "Projects")]
        public List<Tag> Tags {get; set;} = new();


    }
}