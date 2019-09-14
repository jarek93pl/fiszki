using Conector;
using NaukaFiszek.Filter;
using NaukaFiszek.Logic;
using NaukaFiszek.Models;
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
            using (SetFiche setFiche = new SetFiche())
            {
                return setFiche.AddSetFiche(nameSet, UserFiche.CurentUser.Id).ToString();
            }
        }
        [FiszkiAutorize(IsAjaxRequest = true)]
        public ActionResult ListaZestawów()
        {
            using (SetFiche setFiszka = new SetFiche())
            {
                return View(setFiszka.SearchSetsFiche(UserFiche.CurentUser.Id, null));
            }
        }
        [FiszkiAutorize(IsAjaxRequest = true)]
        [HttpPost]
        public void Delete(int id)
        {
            using (SetFiche setFiche = new SetFiche())
            {
                setFiche.Remove(id);
            }
        }

        [FiszkiAutorize(IsAjaxRequest = true)]
        public ActionResult Edytuj(int id)
        {
            SetFicheDetails setFicheDetails = new SetFicheDetails();
            using (Fiche fiche = new Fiche())
            {
                setFicheDetails.Fiches = fiche.SearchFiches(id);
            }
            using (SetFiche setFiche = new SetFiche())
            {
                var fiche = setFiche.SearchSetsFiche(UserFiche.CurentUser.Id, id).First();
                setFicheDetails.Name = fiche.Name;
            }
            return View(setFicheDetails);
        }

    }
}