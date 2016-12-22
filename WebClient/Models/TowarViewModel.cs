using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class TowarViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nazwa")]
        public string Nazwa { get; set; }

        [DisplayName("Dostawca")]
        public int DostawcaId { get; set; }

        [DisplayName("Cena")]
        public decimal Cena { get; set; }

        [DisplayName("Vat")]
        public decimal Vat { get; set; }

        [DisplayName("Ilość")]
        public int StanMagazynowy { get; set; }

        [DisplayName("Wycofany")]
        public bool Wycofany { get; set; }
    }

    public class TowarListaViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nazwa")]
        public string Nazwa { get; set; }

        [DisplayName("Dostawca")]
        public string Dostawca { get; set; }

        [DisplayName("Cena")]
        public decimal Cena { get; set; }

        [DisplayName("Vat")]
        public decimal Vat { get; set; }

        [DisplayName("Ilość")]
        public int StanMagazynowy { get; set; }

        [DisplayName("Wycofany")]
        public bool Wycofany { get; set; }
    }
}