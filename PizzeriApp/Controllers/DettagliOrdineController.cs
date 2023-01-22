using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PizzeriApp;

namespace PizzeriApp.Controllers
{
    public class DettagliOrdineController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: DettagliOrdine
        public ActionResult Index()
        {
        //    Ordini ordini = new Ordini();
        //    ordini.Confermato = ordini.Confermato;
        //    ordini.Evaso = ordini.Evaso;
        //    DettagliOrdine d = new DettagliOrdine();
        //    d.Quantita = DettagliOrdine.Quantita;
            var dettagliOrdine = db.DettagliOrdine.Include(d => d.Ordini).Include(d => d.Pizza).Include(d => d.Utente);
            return View(dettagliOrdine.ToList());
        }

        // GET: DettagliOrdine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliOrdine dettagliOrdine = db.DettagliOrdine.Find(id);
            if (dettagliOrdine == null)
            {
                return HttpNotFound();
            }
            return View(dettagliOrdine);
        }

        // GET: DettagliOrdine/Create
        public ActionResult Create(int id)
        {
            TempData["intPizza"]=id;



            //ViewBag.IdOrdini = new SelectList(db.Ordini, "IDordini", "Note");
            //ViewBag.IdPizza = new SelectList(db.Pizza, "IDpizza", "NomePizza");
            //ViewBag.IdUtente = new SelectList(db.Utente, "IDutente", "Username");
            return View();
        }

        // POST: DettagliOrdine/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDdettagliOrdine,Quantita,IdPizza")] DettagliOrdine dettagliOrdine)
        {
            int ID = (int)TempData["intPizza"];

            if (ModelState.IsValid)
            {
                // gli passo tutto l'oggetto
                var utente = db.Utente.Where(u=>u.Username == User.Identity.Name).First();
                //Utente ut = utente;
                //qui invece mi faccio l'ordine e il dettagliOrdine
                var pizza = db.Pizza.Find(ID);


                DettagliOrdine d = new DettagliOrdine
                {

                    Quantita = dettagliOrdine.Quantita,
                    IdPizza = ID,
                    IdUtente = utente.IDutente,
                    PizzaNome= pizza.NomePizza,
                    NomeUtente= utente.Username,
                    
                };
                //d.Pizza= pizza;
              

                DettagliOrdine.Carrello.Add(d);

         
                //db.DettagliOrdine.Add(dettagliOrdine);
                db.SaveChanges();
                return RedirectToAction("Menu","Pizza");
            }

            ViewBag.IdOrdini = new SelectList(db.Ordini, "IDordini", "Note", dettagliOrdine.IdOrdini);
            ViewBag.IdPizza = new SelectList(db.Pizza, "IDpizza", "NomePizza", dettagliOrdine.IdPizza);
            ViewBag.IdUtente = new SelectList(db.Utente, "IDutente", "Username", dettagliOrdine.IdUtente);
            return View(dettagliOrdine);
        }

        // GET: DettagliOrdine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliOrdine dettagliOrdine = db.DettagliOrdine.Find(id);
            if (dettagliOrdine == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdOrdini = new SelectList(db.Ordini, "IDordini", "Note", dettagliOrdine.IdOrdini);
            ViewBag.IdPizza = new SelectList(db.Pizza, "IDpizza", "NomePizza", dettagliOrdine.IdPizza);
            ViewBag.IdUtente = new SelectList(db.Utente, "IDutente", "Username", dettagliOrdine.IdUtente);
            return View(dettagliOrdine);
        }

        // POST: DettagliOrdine/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDdettagliOrdine,Quantita,IdPizza,IdOrdini,IdUtente")] DettagliOrdine dettagliOrdine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dettagliOrdine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdOrdini = new SelectList(db.Ordini, "IDordini", "Note", dettagliOrdine.IdOrdini);
            ViewBag.IdPizza = new SelectList(db.Pizza, "IDpizza", "NomePizza", dettagliOrdine.IdPizza);
            ViewBag.IdUtente = new SelectList(db.Utente, "IDutente", "Username", dettagliOrdine.IdUtente);
            return View(dettagliOrdine);
        }

        // GET: DettagliOrdine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliOrdine dettagliOrdine = db.DettagliOrdine.Find(id);
            if (dettagliOrdine == null)
            {
                return HttpNotFound();
            }
            return View(dettagliOrdine);
        }

        // POST: DettagliOrdine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DettagliOrdine dettagliOrdine = db.DettagliOrdine.Find(id);
            db.DettagliOrdine.Remove(dettagliOrdine);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Carrello()
        {
            
            return View(DettagliOrdine.Carrello);
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
