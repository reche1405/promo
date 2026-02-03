using RecheApi.Data;

namespace RecheApi.Nifty.Models
{
    public abstract class Model<T> where T : class, new()
    {
        
        public static Manager<T> Objects {get; } = new();

    }
}