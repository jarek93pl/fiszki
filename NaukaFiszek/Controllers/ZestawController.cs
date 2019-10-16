﻿using Conector;
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
        public ActionResult DodajZestaw(string nameSet)
        {
            using (SetFiche setFiche = new SetFiche())
            {
                return Json(new DTO.Models.IdPost(setFiche.AddSetFiche(nameSet, UserFiche.CurentUser.Id)));
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

        [HttpGet]
        [FiszkiAutorize(IsAjaxRequest = true)]
        public ActionResult Edytuj(int id)
        {
            DTO.Models.SetFicheDetails setFicheDetails = new DTO.Models.SetFicheDetails();
            using (Fiche fiche = new Fiche())
            {
                setFicheDetails.Fiches = fiche.SearchFiches(id);
            }
            using (SetFiche setFiche = new SetFiche())
            {
                var fiche = setFiche.SearchSetsFiche(UserFiche.CurentUser.Id, id).First();
                setFicheDetails.Name = fiche.Name;
            }
            setFicheDetails.IdSetFiche = id;
            return View(setFicheDetails);
        }

    }
}