using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class AdresViewModel
    {
        public int Id { get; set; }
        public int KodPocztowyId { get; set; }
        [DisplayName("Kod pocztowy")]
        public string KodPocztowy { get; set; }
        public int MiastoId { get; set; }
        [DisplayName("Miasto")]
        public string Miasto { get; set; }
        public int UlicaId { get; set; }
        [DisplayName("Ulica")]
        public string Ulica { get; set; }
        [DisplayName("Numer domu")]
        public string NumerDomu { get; set; }
        [DisplayName("Numer lokalu")]
        public string NumerLokalu { get; set; }
    }
}