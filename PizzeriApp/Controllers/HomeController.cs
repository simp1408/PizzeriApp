﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PizzeriApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
           
        }
        


    }
}
