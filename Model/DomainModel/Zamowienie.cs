using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class Zamowienie
    {
        public Zamowienie()
        {
            Pozycje = new List<ZamowieniePozycja>();
        }

        public int Id { get; set; }

        public int KlientId { get; set; }
        public virtual Klient Klient { get; set; }

        public DateTime DataZlozeniaZamowienia { get; set; }
        public bool CzyPrzyjetoZamowienie { get; set; }
        public DateTime? DataPrzyjeciaZamowienia { get; set; }

        public bool Zaplacono { get; set; }
        public bool CzyZrealizowanoZamowienie { get; set; }
        public DateTime? DataRealizacjiZamowienia { get; set; }

        public virtual FakturaSprzedazy Faktura { get; set; }

        public virtual ICollection<ZamowieniePozycja> Pozycje { get; set; }
    }
}
