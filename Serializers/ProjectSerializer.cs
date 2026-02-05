using RecheApi.Models;
using RecheApi.Nifty.Serializers;
namespace RecheApi.Serializers
{
    public class ProjectSerializer : ModelSerializer<Project>
    {
        public ProjectSerializer(Nifty.Serializers.Data? data = null) : base(data) {}   
    }
}