using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class FakturaSprzedazy
    {
        public FakturaSprzedazy()
        {
            Pozycje = new List<FakturaSprzedazyPozycja>();
        }

        public int Id { get; set; }
        public Zamowienie Zamowienie { get; set; }
        public int KlientId { get; set; }
        public Klient Klient { get; set; }
        public string NumerFaktury { get; set; }
        public DateTime DataSprzedazy { get; set; }
        public DateTime DataWystawienia { get; set; }
        public decimal KwotaNetto { get; set; }
        public decimal StawkaVat { get; set; }
        public decimal WartoscVat { get; set; }
        public decimal KwotaBrutto { get; set; }

        public virtual ICollection<FakturaSprzedazyPozycja> Pozycje { get; set; }
    }
}
