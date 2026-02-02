using System.ComponentModel.DataAnnotations.Schema;

namespace RecheApi.Models
{
    [Table("Colour")]
    public class Colour() : Model<Colour>
    {
        public int ColourId;
        public int Red {get;set;}
        public int Green {get;set;}
        public int Blue {get;set;}
        public double Alpha {get;set;}
    }
}