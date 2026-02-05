using Microsoft.AspNetCore.Components.Web;
using RecheApi.Models;
using RecheApi.Nifty.Serializers;
using RecheApi.Nifty.Serializers.DataTransfer;
namespace RecheApi.Serializers
{
    public class ProjectSerializer : ModelSerializer<Project>
    {
        public ProjectSerializer(RequestData? data = null) : base(data) {}   
    }
}