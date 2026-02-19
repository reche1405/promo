using RecheApi.Nifty.Migrations.Operations;
namespace RecheApi.Nifty.Migrations
{
    public class Migration()
    {
        public List<Operation> Operations {get;set;} = new(); 
        public List<string> Dependencies {get;set;} = new();

        public DateTime CreatedAt {get;set;} = new();
        
    }
}