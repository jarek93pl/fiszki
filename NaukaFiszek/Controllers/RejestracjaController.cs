using NaukaFiszek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class RejestracjaController : Controller
    {
        public ActionResult Logowanie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logowanie(AuthorizationDetails authorizationDetails)
        {
            return View();
        }
    }
}