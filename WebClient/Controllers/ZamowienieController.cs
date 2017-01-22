using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using Model.DomainModel;
using WebClient.Models;
using Microsoft.Practices.Unity;
using Interfaces;
using MvcRazorToPdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using System.IO;
using iTextSharp.text.pdf;

namespace WebClient.Controllers
{
    public class ZamowienieController : Controller
    {
        [Dependency]
        public IUnitOfWork UnitOfWork { get; set; }

        public async Task<ActionResult> Index()
        {
            var zamowienia = UnitOfWork.Zamowienia.GetAll();

            var model = zamowienia.Select(x => new ZamowienieListaViewModel()
            {
                Id = x.Id,
                Klient = x.Klient.Nazwa,
                NumerFaktury = x.Faktura != null ? x.Faktura.NumerFaktury : "",
                DataZlozeniaZamowienia = x.DataZlozeniaZamowienia,
                CzyPrzyjetoZamowienie = x.CzyPrzyjetoZamowienie,
                DataPrzyjeciaZamowienia = x.DataPrzyjeciaZamowienia,
                Zaplacono = x.Zaplacono,
                CzyZrealizowanoZamowienie = x.CzyZrealizowanoZamowienie,
                DataRealizacjiZamowienia = x.DataRealizacjiZamowienia,
            });

            return View(await model.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Zamowienie zamowienie = await Task.FromResult(UnitOfWork.Zamowienia.GetById(id.Value));

            if (zamowienie == null)
            {
                return HttpNotFound();
            }

            var model = new ZamowienieViewModel()
            {
                Id = zamowienie.Id,
                KlientId = zamowienie.Klient.Id,
                Klient = zamowienie.Klient.Nazwa,
                FakturaId = zamowienie.Faktura != null ? zamowienie.Faktura.Id : (int?)null,
                NumerFaktury = zamowienie.Faktura != null ? zamowienie.Faktura.NumerFaktury : "",
                DataZlozeniaZamowienia = zamowienie.DataZlozeniaZamowienia,
                CzyPrzyjetoZamowienie = zamowienie.CzyPrzyjetoZamowienie,
                DataPrzyjeciaZamowienia = zamowienie.DataPrzyjeciaZamowienia,
                Zaplacono = zamowienie.Zaplacono,
                CzyZrealizowanoZamowienie = zamowienie.CzyZrealizowanoZamowienie,
                DataRealizacjiZamowienia = zamowienie.DataRealizacjiZamowienia,
                Pozycje = zamowienie.Pozycje.Select(pozycja => new PozycjaZamowieniaViewModel() 
                {
                    Id = pozycja.Id,
                    ZamowienieId  = pozycja.Zamowienie.Id,
                    TowarId  = pozycja.Towar.Id,
                    TowarNazwa  = pozycja.Towar.Nazwa,
                    TowarCena = pozycja.Towar.Cena,
                    TowarVat = pozycja.Towar.Vat,
                    Ilosc = pozycja.Ilosc,
                }).ToList()
            };

            return View(model);
        }

        public ActionResult Create()
        {
            var model = new ZamowienieViewModel();
            ViewBag.Klienci = new SelectList(UnitOfWork.Klienci.GetAll(), "Id", "Nazwa");
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create([ModelBinder(typeof(ZamowienieCustomDataBinder))]ZamowienieViewModel zamowienie, string action)
        {
            if (action == "AddPosition")
            {
                ModelState.Clear();
                zamowienie.Pozycje.Add(new PozycjaZamowieniaViewModel());
            }
            else if (action == "Calc")
            {
                ModelState.Clear();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var domainModel = new Zamowienie()
                    {
                        KlientId = zamowienie.KlientId,
                        DataZlozeniaZamowienia = zamowienie.DataZlozeniaZamowienia.Value,
                        CzyPrzyjetoZamowienie = zamowienie.CzyPrzyjetoZamowienie,
                        DataPrzyjeciaZamowienia = zamowienie.DataPrzyjeciaZamowienia,
                        Zaplacono = zamowienie.Zaplacono,
                        CzyZrealizowanoZamowienie = zamowienie.CzyZrealizowanoZamowienie,
                        DataRealizacjiZamowienia = zamowienie.DataRealizacjiZamowienia,
                    };

                    domainModel.Pozycje = zamowienie.Pozycje.Select(pozycja => new ZamowieniePozycja()
                    {
                        Zamowienie = domainModel,
                        TowarId = pozycja.TowarId.Value,
                        Ilosc = pozycja.Ilosc.Value
                    }).ToList();

                    UnitOfWork.Zamowienia.Add(domainModel);
                    await UnitOfWork.CommitAsync();

                    return RedirectToAction("Index");
                } 
            }

            ViewBag.Klienci = new SelectList(UnitOfWork.Klienci.GetAll(), "Id", "Nazwa");
            ViewBag.Towary = new SelectList(UnitOfWork.Towary.GetAll().Where(x => !x.Wycofany), "Id", "Nazwa");
            zamowienie.Pozycje = zamowienie.Pozycje.Select(x => FillTowar(x)).ToList();

            return View(zamowienie);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Zamowienie zamowienie = await Task.FromResult(UnitOfWork.Zamowienia.GetById(id.Value));

            if (zamowienie == null)
            {
                return HttpNotFound();
            }

            var model = new ZamowienieViewModel()
            {
                Id = zamowienie.Id,
                KlientId = zamowienie.Klient.Id,
                Klient = zamowienie.Klient.Nazwa,
                FakturaId = zamowienie.Faktura != null ? zamowienie.Faktura.Id : (int?)null,
                NumerFaktury = zamowienie.Faktura != null ? zamowienie.Faktura.NumerFaktury : "",
                DataZlozeniaZamowienia = zamowienie.DataZlozeniaZamowienia,
                CzyPrzyjetoZamowienie = zamowienie.CzyPrzyjetoZamowienie,
                DataPrzyjeciaZamowienia = zamowienie.DataPrzyjeciaZamowienia,
                Zaplacono = zamowienie.Zaplacono,
                CzyZrealizowanoZamowienie = zamowienie.CzyZrealizowanoZamowienie,
                DataRealizacjiZamowienia = zamowienie.DataRealizacjiZamowienia,
                Pozycje = zamowienie.Pozycje.Select(pozycja => new PozycjaZamowieniaViewModel()
                {
                    Id = pozycja.Id,
                    ZamowienieId = pozycja.Zamowienie.Id,
                    TowarId = pozycja.Towar.Id,
                    TowarNazwa = pozycja.Towar.Nazwa,
                    TowarCena = pozycja.Towar.Cena,
                    TowarVat = pozycja.Towar.Vat,
                    Ilosc = pozycja.Ilosc,
                }).ToList()
            };

            ViewBag.Klienci = new SelectList(UnitOfWork.Klienci.GetAll(), "Id", "Nazwa");
            ViewBag.Towary = new SelectList(UnitOfWork.Towary.GetAll().Where(x => !x.Wycofany), "Id", "Nazwa");

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([ModelBinder(typeof(ZamowienieCustomDataBinder))] ZamowienieViewModel zamowienie, string action)
        {
            if (action == "AddPosition")
            {
                ModelState.Clear();
                zamowienie.Pozycje.Add(new PozycjaZamowieniaViewModel());
            }
            else if (action == "Calc")
            {
                ModelState.Clear();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var domainModel = UnitOfWork.Zamowienia.GetById(zamowienie.Id);
                    domainModel.KlientId = zamowienie.KlientId;
                    domainModel.DataZlozeniaZamowienia = zamowienie.DataZlozeniaZamowienia.Value;
                    domainModel.CzyPrzyjetoZamowienie = zamowienie.CzyPrzyjetoZamowienie;
                    domainModel.DataPrzyjeciaZamowienia = zamowienie.DataPrzyjeciaZamowienia;
                    domainModel.Zaplacono = zamowienie.Zaplacono;
                    domainModel.CzyZrealizowanoZamowienie = zamowienie.CzyZrealizowanoZamowienie;
                    domainModel.DataRealizacjiZamowienia = zamowienie.DataRealizacjiZamowienia;

                    var pozycje = zamowienie.Pozycje.Select(pozycja => new ZamowieniePozycja()
                    {
                        Zamowienie = domainModel,
                        TowarId = pozycja.TowarId.Value,
                        Ilosc = pozycja.Ilosc.Value
                    }).ToList();

                    var toAdd = pozycje.Where(x => x.Id <= 0).ToList();
                    var toUpdate = pozycje.Where(x => domainModel.Pozycje.Any(pozycja => pozycja.Id == x.Id)).ToList();
                    var toDelete = domainModel.Pozycje.Where(pozycja => !pozycje.Any(x => x.Id == pozycja.Id)).ToList();

                    toAdd.ForEach(poz => domainModel.Pozycje.Add(poz));
                    toUpdate.ForEach(poz => 
                    {
                        var pozycja = domainModel.Pozycje.Single(x => x.Id == poz.Id);
                        pozycja.TowarId = poz.TowarId;
                        pozycja.Ilosc = poz.Ilosc;
                    });
                    toDelete.ForEach(poz => 
                    { 
                        UnitOfWork.ZamowieniaPozycje.Delete(domainModel.Pozycje.Single(x => x.Id == poz.Id));
                    });

                    UnitOfWork.Zamowienia.Update(domainModel);
                    await UnitOfWork.CommitAsync();

                    return RedirectToAction("Index");
                } 
            }

            ViewBag.Klienci = new SelectList(UnitOfWork.Klienci.GetAll(), "Id", "Nazwa");
            ViewBag.Towary = new SelectList(UnitOfWork.Towary.GetAll().Where(x => !x.Wycofany), "Id", "Nazwa");
            zamowienie.Pozycje = zamowienie.Pozycje.Select(x => FillTowar(x)).ToList();

            return View(zamowienie);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnitOfWork.Zamowienia.Delete(id.Value);
            await UnitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GenerateInvoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Zamowienie zamowienie = await Task.FromResult(UnitOfWork.Zamowienia.GetById(id.Value));

            if (zamowienie == null)
            {
                return HttpNotFound();
            }

            if (zamowienie.Pozycje == null || zamowienie.Pozycje.Count <= 0)
            {
                return HttpNotFound();
            }

            FakturaSprzedazy faktura = new FakturaSprzedazy()
            {
                KlientId = zamowienie.KlientId,
                Zamowienie = zamowienie,
                NumerFaktury = (UnitOfWork.FakturySprzedazy.GetAll().Where(x => x.DataWystawienia.Year == DateTime.Now.Year).Count() + 1).ToString("000000") + "/" + DateTime.Now.Year,
                DataSprzedazy  = zamowienie.DataZlozeniaZamowienia,
                DataWystawienia = DateTime.Now,
                KwotaNetto = zamowienie.Pozycje.Sum(x => x.Ilosc * x.Towar.Cena),
                WartoscVat = zamowienie.Pozycje.Sum(x => x.Ilosc * x.Towar.Cena * (x.Towar.Vat / 100)),
                KwotaBrutto = zamowienie.Pozycje.Sum(x => x.Ilosc * x.Towar.Cena) + zamowienie.Pozycje.Sum(x => x.Ilosc * x.Towar.Cena * (x.Towar.Vat / 100))
            };

            faktura.Pozycje = zamowienie.Pozycje.Select(x => new FakturaSprzedazyPozycja()
            {
                Towar = x.Towar.Nazwa,
                FakturaSprzedazy = faktura,
                CenaJednostkowa = x.Towar.Cena,
                Ilosc  = x.Ilosc,
                KwotaNetto = x.Ilosc * x.Towar.Cena,
                StawkaVat = x.Towar.Vat,
                WartoscVat = x.Ilosc * x.Towar.Cena * (x.Towar.Vat / 100),
                KwotaBrutto = (x.Ilosc * x.Towar.Cena) + (x.Ilosc * x.Towar.Cena * (x.Towar.Vat / 100))
            }).ToList();


            UnitOfWork.FakturySprzedazy.Add(faktura);
            await UnitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Zamowienie zamowienie = await Task.FromResult(UnitOfWork.Zamowienia.GetById(id.Value));

            if (zamowienie == null)
            {
                return HttpNotFound();
            }

            FakturaSprzedazy faktura = zamowienie.Faktura;

            if (faktura == null)
            {
                return HttpNotFound();
            }

            var model = new FakturaViewModel()
            {
                NumerFaktury = faktura.NumerFaktury,
                Klient = faktura.Klient.Nazwa,
                KlientAdres = faktura.Klient.Adres.ToString(),
                DataSprzedazy = faktura.DataSprzedazy,
                DataWystawienia = faktura.DataWystawienia,
                KwotaNetto = faktura.KwotaNetto,
                WartoscVat = faktura.WartoscVat,
                KwotaBrutto = faktura.KwotaBrutto,
                Pozycje = faktura.Pozycje.Select(pozycja => new PozycjaFakturyViewModel()
                {
                    Towar = pozycja.Towar,
                    CenaJednostkowa = pozycja.CenaJednostkowa,
                    Ilosc = pozycja.Ilosc,
                    KwotaNetto = pozycja.KwotaNetto,
                    StawkaVat = pozycja.StawkaVat,
                    WartoscVat = pozycja.WartoscVat,
                    KwotaBrutto = pozycja.KwotaBrutto
                }).ToList()
            };

            return new PdfActionResult("Invoice", model, (writer, document) => 
            {
                FontFactory.Register(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.TTF"));
                document.NewPage(); 
            
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private PozycjaZamowieniaViewModel FillTowar(PozycjaZamowieniaViewModel x)
        {
            if (x.TowarId.HasValue)
            {
                var towar = UnitOfWork.Towary.GetById(x.TowarId.Value);
                x.TowarNazwa = towar.Nazwa;
                x.TowarCena = towar.Cena;
                x.TowarVat = towar.Vat;
            }
            return x;
        }
    }
}
