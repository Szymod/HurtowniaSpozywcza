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

namespace WebClient.Controllers
{
    public class TowarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Towar
        public async Task<ActionResult> Index()
        {
            var towary = db.Towary;

            var model = towary.Select(x => new TowarListaViewModel()
            {
                Id = x.Id,
                Nazwa = x.Nazwa,
                Dostawca = x.Dostawca.Nazwa,
                Cena = x.Cena,
                Vat = x.Vat,
                StanMagazynowy = x.StanMagazynowy,
                Wycofany = x.Wycofany
            });

            return View(await model.ToListAsync());
        }

        // GET: Towar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Towar/Create
        public ActionResult Create()
        {
            ViewBag.DostawcaId = new SelectList(db.Dostawcy, "Id", "Nazwa");
            return View();
        }

        // POST: Towar/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Towar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Towar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Towar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Towar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
