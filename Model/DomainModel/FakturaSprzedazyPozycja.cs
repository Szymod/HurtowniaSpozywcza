using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModel
{
    public class FakturaSprzedazyPozycja
    {
        public int Id { get; set; }
        public int FakturaSprzedazyId { get; set; }
        public virtual FakturaSprzedazy FakturaSprzedazy { get; set; }
        public string Towar { get; set; }
        public decimal CenaJednostkowa { get; set; }
        public int Ilosc { get; set; }
        public decimal KwotaNetto { get; set; }
        public decimal StawkaVat { get; set; }
        public decimal WartoscVat { get; set; }
        public decimal KwotaBrutto { get; set; }

    }
}
