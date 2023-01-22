using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PizzeriApp;

namespace PizzeriApp.Controllers
{
   
    public class PizzaController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        [Authorize]
        // GET: Pizza
        public ActionResult Index()
        {
            return View();
        }
        //[Authorize(Roles = "admin")]
        public ActionResult ListaPizze()
        {
            
            return View(db.Pizza.ToList());
        }
        [Authorize]
        public ActionResult Menu()
        {
          
            return View(db.Pizza.ToList());
        }


        // GET: Pizza/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizza.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // GET: Pizza/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Pizza/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        

        public ActionResult Create( Pizza pizza, HttpPostedFileBase FileFoto)
        {
           
            if (ModelState.IsValid)
            {
                pizza.Img = FileFoto.FileName;
                FileFoto.SaveAs(Server.MapPath("/img/"+pizza.Img));

                db.Pizza.Add(pizza);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pizza);
        }

        // GET: Pizza/Edit/5
    
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizza.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizza/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Edit( Pizza pizza, HttpPostedFileBase FileUpload)
        {
           
                if (ModelState.IsValid)
                {
                    //pizza.Img = FileUpload.FileName;
                    //FileUpload.SaveAs(Server.MapPath("/img/" + pizza.Img));

                    //db.Pizza.Add(pizza);
                    //db.SaveChanges();
                    //return RedirectToAction("ListaPizze");

                db.Entry(pizza).State=EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaPizze");
            }

            return View(pizza);
        }

        // GET: Pizza/Delete/5
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizza.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizza/Delete/5
        //  actionName["Delete"]fa riferimento all'action in GET sopra
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
   
        public ActionResult DeleteConfirmed(int id)
        {
            Pizza pizza = db.Pizza.Find(id);
            db.Pizza.Remove(pizza);
            db.SaveChanges();
            return RedirectToAction("ListaPizze");
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
