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

namespace WebClient.Controllers
{
    public class KlientController : Controller
    {
        [Dependency]
        public IUnitOfWork UnitOfWork { get; set; }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var klienci = UnitOfWork.Klienci.GetAll().Select(d => new KlientListaViewModel()
            {
                Id = d.Id,
                Nazwa = d.Nazwa,
                Telefon = d.Telefon,
                Adres = d.Adres == null ? "" : d.Adres.KodPocztowy.Kod + " " + d.Adres.Miasto.Nazwa + ", " + d.Adres.Ulica.Nazwa + " " + d.Adres.NumerDomu
            });
            return View(await klienci.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Klient klient = await Task.FromResult(UnitOfWork.Klienci.GetById(id.Value));

            if (klient == null)
            {
                return HttpNotFound();
            }

            var model = new KlientViewModel()
            {
                Id = klient.Id,
                Nazwa = klient.Nazwa,
                Telefon = klient.Telefon,
                Adres = new AdresViewModel()
                {
                    Id = klient.Adres.Id,
                    KodPocztowyId = klient.Adres.KodPocztowyId,
                    KodPocztowy = klient.Adres.KodPocztowy.Kod,
                    MiastoId = klient.Adres.MiastoId,
                    Miasto = klient.Adres.Miasto.Nazwa,
                    UlicaId = klient.Adres.UlicaId,
                    Ulica = klient.Adres.Ulica.Nazwa,
                    NumerDomu = klient.Adres.NumerDomu,
                    NumerLokalu = klient.Adres.NumerLokalu,
                }
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(KlientViewModel klient)
        {
            if (ModelState.IsValid)
            {
                var kodPocztowy = new KodPocztowy() { Kod = klient.Adres.KodPocztowy };
                var miasto = new Miasto() { Nazwa = klient.Adres.Miasto, KodPocztowy = kodPocztowy };
                var ulica = new Ulica() { Nazwa = klient.Adres.Ulica, Miasto = miasto };
                var domainModel = new Klient()
                {
                    Nazwa = klient.Nazwa,
                    Adres = new Adres()
                    {
                        KodPocztowy = kodPocztowy,
                        Miasto = miasto,
                        Ulica = ulica,
                        NumerDomu = klient.Adres.NumerDomu,
                        NumerLokalu = klient.Adres.NumerLokalu
                    },
                    Telefon = klient.Telefon
                };

                UnitOfWork.Klienci.Add(domainModel);
                await UnitOfWork.CommitAsync();
                return RedirectToAction("Index");
            }

            return View(klient);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Klient klient = await Task.FromResult(UnitOfWork.Klienci.GetById(id.Value));

            if (klient == null)
            {
                return HttpNotFound();
            }

            var model = new KlientViewModel()
            {
                Id = klient.Id,
                Nazwa = klient.Nazwa,
                Telefon = klient.Telefon,
                Adres = new AdresViewModel()
                {
                    Id = klient.Adres.Id,
                    KodPocztowyId = klient.Adres.KodPocztowyId,
                    KodPocztowy = klient.Adres.KodPocztowy.Kod,
                    MiastoId = klient.Adres.MiastoId,
                    Miasto = klient.Adres.Miasto.Nazwa,
                    UlicaId = klient.Adres.UlicaId,
                    Ulica = klient.Adres.Ulica.Nazwa,
                    NumerDomu = klient.Adres.NumerDomu,
                    NumerLokalu = klient.Adres.NumerLokalu,
                }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(KlientViewModel klient)
        {
            if (ModelState.IsValid)
            {
                var domainModel = UnitOfWork.Klienci.GetById(klient.Id);

                domainModel.Nazwa = klient.Nazwa;
                domainModel.Adres.KodPocztowy.Kod = klient.Adres.KodPocztowy;
                domainModel.Adres.Miasto.Nazwa = klient.Adres.Miasto;
                domainModel.Adres.NumerDomu = klient.Adres.NumerDomu;
                domainModel.Adres.NumerLokalu = klient.Adres.NumerLokalu;
                domainModel.Adres.Ulica.Nazwa = klient.Adres.Ulica;
                domainModel.Telefon = klient.Telefon;

                UnitOfWork.Klienci.Update(domainModel);
                await UnitOfWork.CommitAsync();
                return RedirectToAction("Index");
            }

            return View(klient);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnitOfWork.Klienci.Delete(id.Value);
            await UnitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
