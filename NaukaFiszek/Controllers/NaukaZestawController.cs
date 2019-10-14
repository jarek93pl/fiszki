#define ToTest
using DTO;
using NaukaFiszek.Filter;
using NaukaFiszek.Logic;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class NaukaZestawController : Controller
    {
        public NaukaZestawController()
        {
#if ToTest
            if (UserFiche.CurentUser == null)
            {
                UserFiche.CurentUser = new UserFiche() { Id = 5 };
            }
#endif
        }
#if !ToTest
        [FiszkiAutorize(IsAjaxRequest = true)]
#endif
        [HttpGet]
        public ActionResult Add(int? id)
        {

            TeachSetFiche teachSetFiche = new TeachSetFiche();
            if (id == null)
            {
                using (Conector.SetFiche setFicheConector = new Conector.SetFiche())
                {
                    teachSetFiche.AvailableSetFiches = setFicheConector.SearchSetsFiche(UserFiche.CurentUser.Id, null);
                }
            }
            else
            {
                teachSetFiche.IdSetFiche = id.Value;
            }
            teachSetFiche.teachBags = new List<TeachBag>();
            return View(teachSetFiche);
        }

#if !ToTest
        [FiszkiAutorize(IsAjaxRequest = true)]     
#endif
        [HttpPost]
        public ActionResult Add(TeachSetFiche fiche)
        {
            using (Conector.TeachSetFiche conector = new Conector.TeachSetFiche())
            {
                conector.Add(fiche);
            }
            return Json(new IdPost(fiche.IdSetFiche));
        }
        [HttpPost]
        public void Delete(int id)
        {
            using (Conector.TeachSetFiche conector = new Conector.TeachSetFiche())
            {
                conector.Delete(id);
            }
        }
        [FiszkiAutorize(IsAjaxRequest = true)]
        public ActionResult List()
        {
            using (Conector.TeachSetFiche conector = new Conector.TeachSetFiche())
            {
                return View(conector.SearchTeachSetsByUser(UserFiche.CurentUser.Id));
            }
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