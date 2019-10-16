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
        [FiszkiAutorize(IsAjaxRequest = true)]
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
                    game.TypeAnswer = randFiche.TypeAnswer;
                    game.LimitTimeSek = randFiche.LimitTimeSek;
                    return (game.TypeAnswer) switch
                    {

                        DTO.Enums.TypeAnswer.WriteTextUserChose => WriteText(game),
                        DTO.Enums.TypeAnswer.WriteText => WriteText(game),
                        DTO.Enums.TypeAnswer.UserChose => UserChose(game),
                        DTO.Enums.TypeAnswer.ChoseOption => UserChose(game),
                        DTO.Enums.TypeAnswer.Hangman => Hangman(game),
                        _ => null
                    };
                }

            }
        }
        [HttpGet]
        public ActionResult Hangman(GameState game)
        {
            HangmanGameState hangmanGame = new HangmanGameState(game);
            if (hangmanGame.IsHangman)
            {
                return UserChose(game);
            }
            return View(new HangmanGameState(hangmanGame));
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
        [FiszkiAutorize(IsAjaxRequest = true)]
        [HttpPost]
        public void SendAnswer(SendAnswerRequest request)
        {
            using (Conector.Game conector = new Conector.Game())
            {
                 conector.SendAnswer(request.idTeachSet, request.IdFiche, request.IsCorrect);
            }
        }
        public ActionResult Common()
        {
            return View();
        }
    }
}