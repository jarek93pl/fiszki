using Conector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class ZestawController : Controller
    {
        public ActionResult DodajZestaw()
        {
            return View();
        }
        [HttpPost]
        public string DodajZestaw(string nameSet)
        {
            SetFiszka setFiszka = new SetFiszka();
            return setFiszka.AddSetFiszka(nameSet).ToString();
        }
    }
}