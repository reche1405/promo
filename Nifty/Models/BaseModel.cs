using RecheApi.Nifty.Attributes.Models;
namespace RecheApi.Nifty.Models
{
    public abstract class BaseModel()
    {
        [Column(autoNowAdd:true, readOnly: true)]
        public DateTime? DateTimeAdded {get;set;}

        [Column(autoNow:true, readOnly: true)]
        public DateTime? LastUpdated {get;set;}

    }
}