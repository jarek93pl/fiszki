using NaukaFiszek.Filter;
using DTO.Models;
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
        public ActionResult Prompt(Fiche fiche)
        {
            return View(fiche);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">numer zestawu uczącego</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NextFicheTeach(int id = 15)
        {
            using (Conector.Game conector = new Conector.Game())
            {
                var randFiche = conector.NextFiche(id);
                GameState game = new GameState();
                using (Conector.Fiche ficheConector = new Conector.Fiche())
                {
                    game.Fiche = ficheConector.LoadFiche(randFiche.IdFiche);
                    game.IdTeachSet = id;
                    game.TypeAnswear = randFiche.TypeAnswear;
                    return (game.TypeAnswear) switch
                    {

                        DTO.Enums.TypeAnswear.WriteTextUserChose => WriteText(game),
                        DTO.Enums.TypeAnswear.WriteText => WriteText(game),
                        DTO.Enums.TypeAnswear.UserChose => UserChose(game),
                        DTO.Enums.TypeAnswear.ChoseOption => UserChose(game),
                        DTO.Enums.TypeAnswear.Hangman => Hangman(game),
                        _ => null
                    };
                }

            }
        }
        [HttpGet]
        public ActionResult Hangman(GameState game)
        {
            return View(new HangmanGameState() { Fiche = new Fiche() { Response = "efwef" } });
        }

        [HttpGet]
        public ActionResult UserChose(GameState game)
        {
            return View(game);
        }
        [HttpGet]
        public ActionResult WriteText(GameState game)
        {
            return View(game);
        }
        [HttpGet]
        public ActionResult ChoseOption(GameState game)
        {
            if (game.Fiche.Response.Length == 0)
            {
                return UserChose(game);
            }
            return View(game);
        }
        //[FiszkiAutorize(IsAjaxRequest = true)]
        [HttpPost]
        public void SendAnswear(SendAnswearRequest request)
        {
            using (Conector.Game conector = new Conector.Game())
            {
               // conector.SendAnswear(request.idTeachSet, request.IdFiche, request.IsCorrect);
            }
        }
        public ActionResult Common()
        {
            return View();
        }
    }
}