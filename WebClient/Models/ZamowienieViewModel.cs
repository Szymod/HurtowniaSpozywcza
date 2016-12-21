using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class ZamowienieViewModel
    {
        public ZamowienieViewModel()
        {
            Pozycje = new List<PozycjaZamowieniaViewModel>();
        }

        public int Id { get; set; }

        [DisplayName("Klient")]
        public int KlientId { get; set; }
        [DisplayName("Data zgłoszenia zamówienia")]
        public DateTime DataZlozeniaZamowienia { get; set; }
        [DisplayName("Czy zamówienie przyjęto?")]
        public bool CzyPrzyjetoZamowienie { get; set; }
        [DisplayName("Data przyjęcia zamówienia")]
        public DateTime? DataPrzyjeciaZamowienia { get; set; }
        [DisplayName("Czy klient zapłacił?")]
        public bool Zaplacono { get; set; }
        [DisplayName("Czy zamówienie zrealizowano?")]
        public bool CzyZrealizowanoZamowienie { get; set; }
        [DisplayName("Data zrealizowania zamówienia")]
        public DateTime? DataRealizacjiZamowienia { get; set; }

        public virtual ICollection<PozycjaZamowieniaViewModel> Pozycje { get; set; }
    }

    public class PozycjaZamowieniaViewModel
    {
        public int Id { get; set; }
        public int ZamowienieId { get; set; }
        public int TowarId { get; set; }
        [DisplayName("Nazwa")]
        public string TowarNazwa { get; set; }
        [DisplayName("Cena")]
        public int TowarCena { get; set; }
        [DisplayName("Ilość")]
        public int Ilosc { get; set; }
    }
}