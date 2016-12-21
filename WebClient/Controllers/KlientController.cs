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
    public class KlientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Klient
        public async Task<ActionResult> Index()
        {
            var klienci = db.Klienci.Include(k => k.Adres);
            return View(await klienci.ToListAsync());
        }

        // GET: Klient/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klient klient = await db.Klienci.FindAsync(id);
            if (klient == null)
            {
                return HttpNotFound();
            }
            return View(klient);
        }

        // GET: Klient/Create
        public ActionResult Create()
        {
            ViewBag.AdresId = new SelectList(db.Adresy, "Id", "NumerDomu");
            return View();
        }

        // POST: Klient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nazwa")] KlientViewModel klient)
        {
            if (ModelState.IsValid)
            {
                //db.Klienci.Add(klient);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AdresId = new SelectList(db.Adresy, "Id", "NumerDomu", klient.AdresId);
            return View(klient);
        }

        // GET: Klient/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klient klient = await db.Klienci.FindAsync(id);
            if (klient == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdresId = new SelectList(db.Adresy, "Id", "NumerDomu", klient.AdresId);
            return View(klient);
        }

        // POST: Klient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nazwa,AdresId")] Klient klient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klient).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AdresId = new SelectList(db.Adresy, "Id", "NumerDomu", klient.AdresId);
            return View(klient);
        }

        // GET: Klient/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klient klient = await db.Klienci.FindAsync(id);
            if (klient == null)
            {
                return HttpNotFound();
            }
            return View(klient);
        }

        // POST: Klient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Klient klient = await db.Klienci.FindAsync(id);
            db.Klienci.Remove(klient);
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
