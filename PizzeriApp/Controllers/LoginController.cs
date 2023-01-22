using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PizzeriApp;
using PizzeriApp.Models;

namespace PizzeriApp.Controllers
{
    public class LoginController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        
        // GET: Login
        public ActionResult Index()
        {
            return View();
           //db.Utente.ToList()
        }

        // GET: Login/Create
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Exclude = "Ruolo")] Utente utente)
        {


            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["ModelDbContext"].ToString();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("Username", utente.Username);
                command.Parameters.AddWithValue("Password", utente.Password);
                command.CommandText = "select Username,Password from Utente where Username=@Username and Password=@Password";
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();

            //db.Utente.Where(u=>u.Username== utente.Username && u.Password == utente.Password).Count()==1;
                if (reader.HasRows)
                {

                    FormsAuthentication.SetAuthCookie(utente.Username, false);
                    Utente u = db.Utente.Where(x => x.Username == utente.Username).FirstOrDefault();
                    
                    if (u.Ruolo=="admin") 
                    { return RedirectToAction("ListaPizze","Pizza"); }
                
                    else
                    {

                    return Redirect(FormsAuthentication.DefaultUrl);

                    }
                       
                }
                else
                {
                    ViewBag.ErrorMessage = "Username e/o Password errate";
                    return View();
                }

            }
            catch (Exception ex)
            { 
                return null; }
            finally { 
                
                connection.Close(); 
            }


            //return View(db.Pizza.ToList());
        }

       public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
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
