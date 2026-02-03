using RecheApi.Nifty.Attributes.Models;
using RecheApi.Nifty.Models;

namespace RecheApi.Models
{
    [Table("ProjectTag"), Join("Project", "Tag")]
    public class ProjectTag() : Model<ProjectTag>
    {
        [Column(required:true, foreignKey: "Project" )]
        public int ProjectId {get;set;}
        [Column(required:true, foreignKey: "Tag" )]
        public int TagId {get;set;}

    }
}