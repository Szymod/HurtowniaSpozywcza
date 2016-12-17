using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class KodPocztowy
    {
        public KodPocztowy()
        {
            Adresy = new List<Adres>();
            Miasta = new List<Miasto>();
        }

        public int Id { get; set; }
        public string Kod { get; set; }

        public virtual ICollection<Miasto> Miasta { get; set; }
        public virtual ICollection<Adres> Adresy { get; set; }
    }
}
