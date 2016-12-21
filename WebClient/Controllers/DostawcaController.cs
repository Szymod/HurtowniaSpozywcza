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

namespace WebClient.Controllers
{
    public class DostawcaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dostawca
        public async Task<ActionResult> Index()
        {
            var dostawcy = db.Dostawcy.Include(d => d.Adres);
            return View(await dostawcy.ToListAsync());
        }

        // GET: Dostawca/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dostawca dostawca = await db.Dostawcy.FindAsync(id);
            if (dostawca == null)
            {
                return HttpNotFound();
            }
            return View(dostawca);
        }

        // GET: Dostawca/Create
        public ActionResult Create()
        {
            ViewBag.AdresId = new SelectList(db.Adresy, "Id", "NumerDomu");
            return View();
        }

        // POST: Dostawca/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nazwa,AdresId")] Dostawca dostawca)
        {
            if (ModelState.IsValid)
            {
                db.Dostawcy.Add(dostawca);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AdresId = new SelectList(db.Adresy, "Id", "NumerDomu", dostawca.AdresId);
            return View(dostawca);
        }

        // GET: Dostawca/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dostawca dostawca = await db.Dostawcy.FindAsync(id);
            if (dostawca == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdresId = new SelectList(db.Adresy, "Id", "NumerDomu", dostawca.AdresId);
            return View(dostawca);
        }

        // POST: Dostawca/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nazwa,AdresId")] Dostawca dostawca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dostawca).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AdresId = new SelectList(db.Adresy, "Id", "NumerDomu", dostawca.AdresId);
            return View(dostawca);
        }

        // GET: Dostawca/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dostawca dostawca = await db.Dostawcy.FindAsync(id);
            if (dostawca == null)
            {
                return HttpNotFound();
            }
            return View(dostawca);
        }

        // POST: Dostawca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Dostawca dostawca = await db.Dostawcy.FindAsync(id);
            db.Dostawcy.Remove(dostawca);
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
