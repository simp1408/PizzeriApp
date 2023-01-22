using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzeriApp;

namespace PizzeriApp.Controllers
{
    public class OrdiniController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Ordini
        public ActionResult Index()
        {
            var ordini = db.Ordini.Include(o => o.Utente);
            return View(ordini.ToList());
        }

        // GET: Ordini/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordini ordini = db.Ordini.Find(id);
            if (ordini == null)
            {
                return HttpNotFound();
            }
            return View(ordini);
        }

        // GET: Ordini/Create
        public ActionResult Create()
        {
            ViewBag.IdUser = new SelectList(db.Utente, "IDutente", "Username");
            return View();
        }

        // POST: Ordini/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ordini ordini)
        {
            if (ModelState.IsValid)
            {
                Ordini o = new Ordini
                {
                    Evaso = false,
                    Confermato = false,
                    Note = ordini.Note

                };
                

              
               

                db.Ordini.Add(ordini);
                db.SaveChanges();
                return RedirectToAction("Create","DettagliOrdine", new { id = o.IDordini });
            }

            // ViewBag.IdUser = new SelectList(db.Utente, "IDutente", "Username", ordini.IdUser);

            return View(ordini);
        }

        // GET: Ordini/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordini ordini = db.Ordini.Find(id);
            if (ordini == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.Utente, "IDutente", "Username", ordini.IdUser);
            return View(ordini);
        }

        // POST: Ordini/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDordini,Confermato,Note,Evaso,IdUser")] Ordini ordini)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordini).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.Utente, "IDutente", "Username", ordini.IdUser);
            return View(ordini);
        }

        // GET: Ordini/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordini ordini = db.Ordini.Find(id);
            if (ordini == null)
            {
                return HttpNotFound();
            }
            return View(ordini);
        }

        // POST: Ordini/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ordini ordini = db.Ordini.Find(id);
            db.Ordini.Remove(ordini);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PartialViewOrdine()
        {

            return PartialView("_PartialViewOrdine");
        }
        [HttpPost]
        public ActionResult PartialViewOrdine(Ordini ordini)
        {
            Ordini ord = new Ordini();
            {
                ord.Note = ordini.Note;
            }

            db.Ordini.Add(ord);
            db.SaveChanges();

            foreach (DettagliOrdine item in DettagliOrdine.Carrello)
            {
                item.IdOrdini = ord.IDordini;
                db.DettagliOrdine.Add(item);
                db.SaveChanges();
            }
            DettagliOrdine.Carrello.Clear();
            
            return RedirectToAction("Menu","Pizza");
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
