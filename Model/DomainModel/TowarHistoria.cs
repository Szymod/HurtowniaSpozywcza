using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class TowarHistoria
    {
        public int Id { get; set; }
        public int TowarId { get; set; }
        public Towar Towar { get; set; }
        public DateTime DataPoczatkuObowiazywania { get; set; }
        public DateTime DataKoncaObowiazywania { get; set; }
        public decimal Cena { get; set; }
        public decimal Vat { get; set; }
    }
}
