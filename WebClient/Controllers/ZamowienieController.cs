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
    public class ZamowienieController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Zamowienie
        public async Task<ActionResult> Index()
        {
            var zamowienia = db.Zamowienia.Include(z => z.Faktura).Include(z => z.Klient);
            return View(await zamowienia.ToListAsync());
        }

        // GET: Zamowienie/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zamowienie zamowienie = await db.Zamowienia.FindAsync(id);
            if (zamowienie == null)
            {
                return HttpNotFound();
            }
            return View(zamowienie);
        }

        // GET: Zamowienie/Create
        public ActionResult Create()
        {
            var model = new ZamowienieViewModel();
            ViewBag.Id = new SelectList(db.FakturySprzedazy, "Id", "NumerFaktury");
            ViewBag.KlientId = new SelectList(db.Klienci, "Id", "Nazwa");
            return View(model);
        }

        // POST: Zamowienie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,KlientId,DataZlozeniaZamowienia,CzyPrzyjetoZamowienie,DataPrzyjeciaZamowienia,Zaplacono,CzyZrealizowanoZamowienie,DataRealizacjiZamowienia")] Zamowienie zamowienie)
        {
            if (ModelState.IsValid)
            {
                db.Zamowienia.Add(zamowienie);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.FakturySprzedazy, "Id", "NumerFaktury", zamowienie.Id);
            ViewBag.KlientId = new SelectList(db.Klienci, "Id", "Nazwa", zamowienie.KlientId);
            return View(zamowienie);
        }

        // GET: Zamowienie/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zamowienie zamowienie = await db.Zamowienia.FindAsync(id);
            if (zamowienie == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.FakturySprzedazy, "Id", "NumerFaktury", zamowienie.Id);
            ViewBag.KlientId = new SelectList(db.Klienci, "Id", "Nazwa", zamowienie.KlientId);
            return View(zamowienie);
        }

        // POST: Zamowienie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,KlientId,DataZlozeniaZamowienia,CzyPrzyjetoZamowienie,DataPrzyjeciaZamowienia,Zaplacono,CzyZrealizowanoZamowienie,DataRealizacjiZamowienia")] Zamowienie zamowienie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zamowienie).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.FakturySprzedazy, "Id", "NumerFaktury", zamowienie.Id);
            ViewBag.KlientId = new SelectList(db.Klienci, "Id", "Nazwa", zamowienie.KlientId);
            return View(zamowienie);
        }

        // GET: Zamowienie/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zamowienie zamowienie = await db.Zamowienia.FindAsync(id);
            if (zamowienie == null)
            {
                return HttpNotFound();
            }
            return View(zamowienie);
        }

        // POST: Zamowienie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Zamowienie zamowienie = await db.Zamowienia.FindAsync(id);
            db.Zamowienia.Remove(zamowienie);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
