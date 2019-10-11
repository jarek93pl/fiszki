using NaukaFiszek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class GraController : Controller
    {
        [HttpGet]
        public ActionResult Prompt(DTO.Fiche fiche)
        {
            fiche = new DTO.Fiche();
            fiche.IdPromptFile = 1020;
            fiche.TypePrompt = DTO.Enums.ContentType.Sound;
            return View(fiche);
        }
        [HttpGet]
        public ActionResult WriteText(GameState game)
        {
            using (Conector.Fiche fiche = new Conector.Fiche())
            {
                //var currentFiche = fiche.LoadFiche(idFiche);
                //GameState game = new GameState();
                //game.IsMultiPlayer = isMultiPlayer;
                //return View();
            }
            throw new NotImplementedException();
        }

    }
}