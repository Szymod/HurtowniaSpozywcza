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
    public class TowarController : Controller
    {
        [Dependency]
        public IUnitOfWork UnitOfWork { get; set; }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var towary = UnitOfWork.Towary.GetAll().Select(x => new TowarListaViewModel()
            {
                Id = x.Id,
                Nazwa = x.Nazwa,
                Dostawca = x.Dostawca.Nazwa,
                Cena = x.Cena,
                Vat = x.Vat,
                StanMagazynowy = x.StanMagazynowy,
                Wycofany = x.Wycofany
            });

            return View(await towary.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            Towar towar = await Task.FromResult(UnitOfWork.Towary.GetById(id.Value));

            if (towar == null)
            {
                return HttpNotFound();
            }

            var model = new TowarViewModel()
            {
                Id = towar.Id,
                Nazwa = towar.Nazwa,
                DostawcaId = towar.DostawcaId,
                Cena = towar.Cena,
                Vat = towar.Vat,
                StanMagazynowy = towar.StanMagazynowy,
                Wycofany = towar.Wycofany
            };

            ViewBag.Dostawca = UnitOfWork.Dostawcy.GetById(towar.DostawcaId).Nazwa;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Dostawcy = new SelectList(UnitOfWork.Dostawcy.GetAll(), "Id", "Nazwa");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TowarViewModel towar)
        {
            if (ModelState.IsValid)
            {
                var domainModel = new Towar()
                {
                    Nazwa = towar.Nazwa,
                    DostawcaId = towar.DostawcaId,
                    Cena = towar.Cena,
                    Vat = towar.Vat,
                    StanMagazynowy = towar.StanMagazynowy,
                    Wycofany = towar.Wycofany
                };

                UnitOfWork.Towary.Add(domainModel);
                await UnitOfWork.CommitAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Dostawcy = new SelectList(UnitOfWork.Dostawcy.GetAll(), "Id", "Nazwa");
            return View(towar);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Dostawcy = new SelectList(UnitOfWork.Dostawcy.GetAll(), "Id", "Nazwa");
            Towar towar = await Task.FromResult(UnitOfWork.Towary.GetById(id.Value));

            if (towar == null)
            {
                return HttpNotFound();
            }

            var model = new TowarViewModel()
            {
                Id = towar.Id,
                Nazwa = towar.Nazwa,
                DostawcaId = towar.DostawcaId,
                Cena = towar.Cena,
                Vat = towar.Vat,
                StanMagazynowy = towar.StanMagazynowy,
                Wycofany = towar.Wycofany
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TowarViewModel towar)
        {
            if (ModelState.IsValid)
            {
                var domainModel = UnitOfWork.Towary.GetById(towar.Id);

                domainModel.Nazwa = towar.Nazwa;
                domainModel.DostawcaId = towar.DostawcaId;
                domainModel.Cena = towar.Cena;
                domainModel.Vat = towar.Vat;
                domainModel.StanMagazynowy = towar.StanMagazynowy;
                domainModel.Wycofany = towar.Wycofany;

                UnitOfWork.Towary.Update(domainModel);
                await UnitOfWork.CommitAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Dostawcy = new SelectList(UnitOfWork.Dostawcy.GetAll(), "Id", "Nazwa");
            return View(towar);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UnitOfWork.Towary.Delete(id.Value);
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
