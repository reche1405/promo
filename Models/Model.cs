using RecheApi.Data;

namespace RecheApi.Models
{
    public abstract class Model<T> where T : class, new()
    {
        Manager<T> Objects {get; } = new();
        
    }
}