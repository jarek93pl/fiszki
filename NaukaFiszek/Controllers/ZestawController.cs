using Conector;
using NaukaFiszek.Filter;
using NaukaFiszek.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class ZestawController : Controller
    {
        [FiszkiAutorize(IsAjaxRequest = true)]
        public ActionResult DodajZestaw()
        {
            return PartialView();
        }
        [FiszkiAutorize(IsAjaxRequest = true)]
        [HttpPost]
        public string DodajZestaw(string nameSet)
        {
            SetFiche setFiche = new SetFiche();
            return setFiche.AddSetFiche(nameSet, UserFiche.CurentUser.Id).ToString();
        }
        [FiszkiAutorize(IsAjaxRequest = true)]
        public ActionResult ListaZestawów()
        {
            SetFiche setFiszka = new SetFiche();
            return View(setFiszka.SearchSetsFiche(UserFiche.CurentUser.Id));
        }
        [HttpPost]
        public void Delete(int id)
        {
            SetFiche setFiche = new SetFiche();
            setFiche.Remove(id);
        }
    }
}