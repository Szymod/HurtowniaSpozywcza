using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class FakturaViewModel
    {
        public FakturaViewModel()
        {
            Pozycje = new List<PozycjaFakturyViewModel>();
        }

        public string Klient { get; set; }
        public string KlientAdres { get; set; }

        public string NumerFaktury { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataSprzedazy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataWystawienia { get; set; }

        [DisplayName("Kwota Netto")]
        public decimal KwotaNetto { get; set; }

        [DisplayName("Wartość VAT")]
        public decimal WartoscVat { get; set; }

        [DisplayName("Kwota Brutto")]
        public decimal KwotaBrutto { get; set; }


        public virtual IList<PozycjaFakturyViewModel> Pozycje { get; set; }
    }

    public class PozycjaFakturyViewModel
    {
        public string Towar { get; set; }

        [DisplayName("Cena")]
        public decimal CenaJednostkowa { get; set; }

        [DisplayName("Ilość")]
        public int Ilosc { get; set; }

        [DisplayName("Kwota Netto")]
        public decimal KwotaNetto { get; set; }

        [DisplayName("Stawka VAT")]
        public decimal StawkaVat { get; set; }

        [DisplayName("Wartość VAT")]
        public decimal WartoscVat { get; set; }

        [DisplayName("Kwota Brutto")]
        public decimal KwotaBrutto { get; set; }
    }
}