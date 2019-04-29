using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Souvenir
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Popularity { get; set; }
        public string PathOfImage { get; set; }
        public int CategoryID { get; set; }
    }
}
