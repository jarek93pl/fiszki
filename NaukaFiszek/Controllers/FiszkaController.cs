using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class FiszkaController : Controller
    {
        // GET: Fiszka
        public ActionResult Index()
        {
            return View();
        }
    }
}