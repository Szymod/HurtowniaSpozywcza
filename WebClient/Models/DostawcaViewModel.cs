using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class DostawcaViewModel
    {
        public int Id { get; set; }
        [DisplayName("Nazwa dostawcy")]
        public string Nazwa { get; set; }
        [DisplayName("Telefon kontaktowy")]
        public string Telefon { get; set; }
        public int AdresId { get; set; }
        [DisplayName("Adres")]
        public AdresViewModel Adres { get; set; }
    }
}