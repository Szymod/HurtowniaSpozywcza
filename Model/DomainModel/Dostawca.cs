using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class Dostawca
    {
        public Dostawca()
        {
            Towary = new List<Towar>();
        }

        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Telefon { get; set; }
        public int AdresId { get; set; }
        public Adres Adres { get; set; }

        public virtual ICollection<Towar> Towary { get; set; }
    }
}
