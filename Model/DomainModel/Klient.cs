using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class Klient
    {
        public Klient()
        {
            Zamowienia = new List<Zamowienie>();
            Faktury = new List<FakturaSprzedazy>();
        }

        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Telefon { get; set; }
        public int AdresId { get; set; }
        public virtual Adres Adres { get; set; }

        public virtual ICollection<Zamowienie> Zamowienia { get; set; }
        public virtual ICollection<FakturaSprzedazy> Faktury { get; set; }
    }
}
