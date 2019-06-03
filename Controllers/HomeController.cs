using SpacebookSpa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpacebookSpa.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult BusinessIndex()
        {
            return View();
      
        }

        public ActionResult UserIndex()
        {
            return View();
        }

    }
}