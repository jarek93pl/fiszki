//#define ToTest
using DTO;
using DTO.Enums;
using NaukaFiszek.Filter;
using NaukaFiszek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class FiszkaController : Controller
    {
#if !ToTest
        [FiszkiAutorize(IsAjaxRequest = true)]
#endif
        public ActionResult Dodaj(
#if !ToTest
            int idFicheSet
#endif
            )
        {
            return View(new Fiche()
            {
#if !ToTest
                IdFicheSet = idFicheSet,
#endif
                FicheResponses = new FicheResponse[]
                {
#if ToTest
                    new FicheResponse(){Id=1,Name="ble",IsCorect=true},
                    new FicheResponse(){Id=2,Name="ble ble"}
#endif
                }
            }
            );
        }
        public ActionResult Edit(int id)
        {
            using (Conector.Fiche ficheConector = new Conector.Fiche())
            {
                var fiche = ficheConector.LoadFiche(id);
                return View("Dodaj", fiche);
            }
        }
        [HttpPost]
        public void DeleteResponse(IdPost fiche)
        {
            using (Conector.Fiche ficheConector = new Conector.Fiche())
            {
                ficheConector.DeleteResponse(fiche.id);
            }
        }
        [HttpPost]
        public void Dodaj(Fiche fiche)
        {
            using (Conector.Fiche ficheConector = new Conector.Fiche())
            {
                ficheConector.SendFiche(fiche);
            }
        }
        [HttpGet]
        public ActionResult ResponseEditorWithData(int? idFicheSet)
        {
            return View(new FicheResponse()
            {
                Id = idFicheSet ?? 0
            });
        }
        [HttpGet]
        public ActionResult ResponseRow()
        {
            return View(new FicheResponse());
        }
        [HttpGet]
        public ActionResult ResponseRowWithData(FicheResponse fiche)
        {
            return View("ResponseRow", fiche);
        }
    }
}