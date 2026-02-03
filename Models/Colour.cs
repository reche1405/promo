using RecheApi.Nifty.Attributes.Models;
using RecheApi.Nifty.Models;

namespace RecheApi.Models
{
    [Table("Colour")]
    public class Colour() : Model<Colour>
    {
        [Column(primaryKey: true, autoIncrement: true)]
        public int ColourId {get;set;}
        [Column]
        public int Red {get;set;}
        [Column]
        public int Green {get;set;}
        [Column]
        public int Blue {get;set;}
        [Column]
        public double Alpha {get;set;}
    }
}