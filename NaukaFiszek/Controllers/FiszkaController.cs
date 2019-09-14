using NaukaFiszek.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class FiszkaController : Controller
    {
        //[FiszkiAutorize(IsAjaxRequest = true)]
        public ActionResult Dodaj()
        {
            return View();
        }
    }
}