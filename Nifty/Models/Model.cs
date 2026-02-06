
namespace RecheApi.Nifty.Models
{
    public abstract class Model<T> : BaseModel where T : class, new()
    {  
        public static Manager<T> Objects {get; } = new();
        
    }
}