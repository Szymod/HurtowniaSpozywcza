using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [Required]
        public int KlientId { get; set; }

        [DisplayName("Klient")]
        public string Klient { get; set; }

        [DisplayName("Numer faktury")]
        public int? FakturaId { get; set; }

        [DisplayName("Numer faktury")]
        public string NumerFaktury { get; set; }

        [DisplayName("Data zgłoszenia zamówienia")]
        [Required]
        public DateTime? DataZlozeniaZamowienia { get; set; }

        [DisplayName("Czy zamówienie przyjęto?")]
        [Required]
        public bool CzyPrzyjetoZamowienie { get; set; }

        [DisplayName("Data przyjęcia zamówienia")]
        public DateTime? DataPrzyjeciaZamowienia { get; set; }

        [DisplayName("Czy klient zapłacił?")]
        [Required]
        public bool Zaplacono { get; set; }

        [DisplayName("Czy zamówienie zrealizowano?")]
        public bool CzyZrealizowanoZamowienie { get; set; }

        [DisplayName("Data zrealizowania zamówienia")]
        public DateTime? DataRealizacjiZamowienia { get; set; }

        [DisplayName("Suma netto")]
        public decimal? SumaNetto
        {
            get
            {
                return Pozycje.Sum(x => x.Netto);
            }
        }

        [DisplayName("Suma vat")]
        public decimal? SumaVat
        {
            get
            {
                return Pozycje.Sum(x => x.Vat);
            }
        }

        [DisplayName("Suma brutto")]
        public decimal? SumaBrutto
        {
            get
            {
                return Pozycje.Sum(x => x.Brutto);
            }
        }


        public virtual IList<PozycjaZamowieniaViewModel> Pozycje { get; set; }
    }

    public class PozycjaZamowieniaViewModel
    {
        public int Id { get; set; }
        public int ZamowienieId { get; set; }

        [Required]
        public int? TowarId { get; set; }

        [DisplayName("Nazwa")]
        public string TowarNazwa { get; set; }

        [DisplayName("Ilość")]
        [Required]
        public int? Ilosc { get; set; }

        [DisplayName("Cena jednostkowa")]
        public decimal? TowarCena { get; set; }

        [DisplayName("Vat")]
        public decimal? TowarVat { get; set; }
        
        [DisplayName("Wartość netto")]
        public decimal? Netto
        {
            get
            {
                return Ilosc * TowarCena;
            }
        }

        [DisplayName("Wartość vat")]
        public decimal? Vat
        {
            get
            {
                return Netto * (TowarVat / 100);
            }

        }

        [DisplayName("Wartość brutto")]
        public decimal? Brutto 
        { 
            get 
            {
                return Netto + Vat;
            } 
        
        }  
    }

    public class ZamowienieListaViewModel
    {
        public int Id { get; set; }

        [DisplayName("Klient")]
        public string Klient { get; set; }

        [DisplayName("Numer faktury")]
        public string NumerFaktury { get; set; }

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
    }

    public class ZamowienieCustomDataBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            
                HttpRequestBase request = controllerContext.HttpContext.Request;
                ZamowienieViewModel model = (ZamowienieViewModel)base.BindModel(controllerContext, bindingContext);

                model.Pozycje = new List<PozycjaZamowieniaViewModel>();
                var pozycjeKyes = request.Form.AllKeys
                    .Where(x => x.StartsWith("Pozycje"))
                    .GroupBy(x => x.Substring(0, x.IndexOf("]") +1), x => x)
                    .ToList();

                foreach (var pozycja in pozycjeKyes)
                {
                    model.Pozycje.Add(new PozycjaZamowieniaViewModel()
                    {
                        Id = string.IsNullOrEmpty(request.Form[pozycja.Key + ".Id"]) ? 0 : int.Parse(request.Form[pozycja.Key + ".Id"]),
                        TowarId = string.IsNullOrEmpty(request.Form[pozycja.Key + ".TowarId"]) ? null : (int?)int.Parse(request.Form[pozycja.Key + ".TowarId"]),
                        Ilosc = string.IsNullOrEmpty(request.Form[pozycja.Key + ".Ilosc"]) ? null : (int?)int.Parse(request.Form[pozycja.Key + ".Ilosc"]),
                        ZamowienieId = string.IsNullOrEmpty(request.Form[pozycja.Key + ".ZamowienieId"]) ? 0 : int.Parse(request.Form[pozycja.Key + ".ZamowienieId"]),
                    });
                }
                return model;
        }

    } 
}