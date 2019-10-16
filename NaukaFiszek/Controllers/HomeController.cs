using NaukaFiszek.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [FiszkiAutorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ErrorAlert()
        {
            return View();
        }
    }
}