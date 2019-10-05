#define ToTest
using DTO;
using NaukaFiszek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class NaukaZestawController : Controller
    {
        [HttpGet]
        public ActionResult Dodaj(
#if !ToTest
            int id
#endif
            )
        {

            TeachSetFiche teachSetFiche = new TeachSetFiche();
            teachSetFiche.teachBags = new List<TeachBag>();
            return View(teachSetFiche);
        }
        [HttpPost]
        public ActionResult Dodaj(TeachSetFiche fiche)
        {
            return Json(new IdPost());
        }
        public ActionResult BagRow()
        {
            return View();
        }
        public ActionResult BagEditor()
        {
            return View();
        }

    }
}