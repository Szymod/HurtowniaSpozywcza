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
    public class DostawcaController : Controller
    {
        [Dependency]
        public IUnitOfWork UnitOfWork { get; set; }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var dostawcy = UnitOfWork.Dostawcy.GetAll().Select(d => new DostawcaListaViewModel()
            {
                Id = d.Id,
                Nazwa = d.Nazwa,
                Telefon = d.Telefon,
                Adres = d.Adres == null ? "" : d.Adres.KodPocztowy.Kod + " " + d.Adres.Miasto.Nazwa + ", " + d.Adres.Ulica.Nazwa + " " + d.Adres.NumerDomu
            });
            return View(await dostawcy.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dostawca dostawca = await Task.FromResult(UnitOfWork.Dostawcy.GetById(id.Value));
            

            if (dostawca == null)
            {
                return HttpNotFound();
            }

            var model = new DostawcaViewModel()
            {
                Id = dostawca.Id,
                Nazwa = dostawca.Nazwa,
                Telefon = dostawca.Telefon,
                Adres = new AdresViewModel()
                {
                    Id = dostawca.Adres.Id,
                    KodPocztowyId = dostawca.Adres.KodPocztowyId,
                    KodPocztowy = dostawca.Adres.KodPocztowy.Kod,
                    MiastoId = dostawca.Adres.MiastoId,
                    Miasto = dostawca.Adres.Miasto.Nazwa,
                    UlicaId = dostawca.Adres.UlicaId,
                    Ulica = dostawca.Adres.Ulica.Nazwa,
                    NumerDomu = dostawca.Adres.NumerDomu,
                    NumerLokalu = dostawca.Adres.NumerLokalu,
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
        public async Task<ActionResult> Create(DostawcaViewModel dostawca)
        {
            if (ModelState.IsValid)
            {
                var kodPocztowy = new KodPocztowy() { Kod = dostawca.Adres.KodPocztowy };
                var miasto = new Miasto() { Nazwa = dostawca.Adres.Miasto, KodPocztowy = kodPocztowy };
                var ulica = new Ulica() { Nazwa = dostawca.Adres.Ulica, Miasto = miasto };
                var domainModel = new Dostawca()
                {
                    Nazwa = dostawca.Nazwa,
                    Adres = new Adres() 
                    {
                        KodPocztowy = kodPocztowy,
                        Miasto = miasto,
                        Ulica = ulica,
                        NumerDomu = dostawca.Adres.NumerDomu,
                        NumerLokalu = dostawca.Adres.NumerLokalu
                    },
                    Telefon = dostawca.Telefon
                };

                UnitOfWork.Dostawcy.Add(domainModel);
                await UnitOfWork.CommitAsync();
                return RedirectToAction("Index");
            }

            return View(dostawca);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dostawca dostawca = await Task.FromResult(UnitOfWork.Dostawcy.GetById(id.Value));

            if (dostawca == null)
            {
                return HttpNotFound();
            }

            var model = new DostawcaViewModel()
            {
                Id = dostawca.Id,
                Nazwa = dostawca.Nazwa,
                Telefon = dostawca.Telefon,
                Adres = new AdresViewModel()
                {
                    Id = dostawca.Adres.Id,
                    KodPocztowyId = dostawca.Adres.KodPocztowyId,
                    KodPocztowy = dostawca.Adres.KodPocztowy.Kod,
                    MiastoId = dostawca.Adres.MiastoId,
                    Miasto = dostawca.Adres.Miasto.Nazwa,
                    UlicaId = dostawca.Adres.UlicaId,
                    Ulica = dostawca.Adres.Ulica.Nazwa,
                    NumerDomu = dostawca.Adres.NumerDomu,
                    NumerLokalu = dostawca.Adres.NumerLokalu,
                }
            };

            return View(model);
        }

        [HttpPost]  
        public async Task<ActionResult> Edit(DostawcaViewModel dostawca)
        {
            if (ModelState.IsValid)
            {
                var domainModel = UnitOfWork.Dostawcy.GetById(dostawca.Id);

                domainModel.Nazwa = dostawca.Nazwa;
                domainModel.Adres.KodPocztowy.Kod = dostawca.Adres.KodPocztowy;
                domainModel.Adres.Miasto.Nazwa = dostawca.Adres.Miasto;
                domainModel.Adres.NumerDomu = dostawca.Adres.NumerDomu;
                domainModel.Adres.NumerLokalu = dostawca.Adres.NumerLokalu;
                domainModel.Adres.Ulica.Nazwa = dostawca.Adres.Ulica;
                domainModel.Telefon = dostawca.Telefon;

                UnitOfWork.Dostawcy.Update(domainModel);
                await UnitOfWork.CommitAsync();
                return RedirectToAction("Index");
            }

            return View(dostawca);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnitOfWork.Dostawcy.Delete(id.Value);
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
