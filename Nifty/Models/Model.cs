using RecheApi.Nifty.Attributes.Models;

namespace RecheApi.Nifty.Models
{
    public abstract class Model<T> where T : class, new()
    {
        
        public static Manager<T> Objects {get; } = new();

        [Column(autoNowAdd:true, readOnly: true)]
        public string? DateTimeAdded {get;set;}

        [Column(autoNow:true, readOnly: true)]
        public string? LastUpdated {get;set;}

    }
}