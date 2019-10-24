using NaukaFiszek.Filter;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaukaFiszek.Logic;
using DTO.Enums;

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
        public ActionResult NextFicheTeach(int id)
        {
            using (Conector.Game conector = new Conector.Game())
            {
                var randFiche = conector.NextFiche(id);
                if (randFiche.IdFiche != -1)
                {
                    GameState game = new GameState();
                    using (Conector.Fiche ficheConector = new Conector.Fiche())
                    {
                        game.Fiche = ficheConector.LoadFiche(randFiche.IdFiche);
                        game.IdTeachSet = id;
                        game.TypeAnswer = randFiche.TypeAnswer;
                        game.LimitTimeSek = randFiche.LimitTimeSek;
                        if (!game.TypeAnswer.Can(game.Fiche))
                        {
                            return UserChose(game);
                        }
                        return (game.TypeAnswer) switch
                        {

                            DTO.Enums.TypeAnswer.WriteTextUserChose => WriteText(game),
                            DTO.Enums.TypeAnswer.WriteText => WriteText(game),
                            DTO.Enums.TypeAnswer.UserChose => UserChose(game),
                            DTO.Enums.TypeAnswer.ChoseOption => ChoseOption(game),
                            DTO.Enums.TypeAnswer.Hangman => Hangman(game),
                            _ => null
                        };
                    }
                }
                else
                {
                    return GameEnd();
                }

            }
        }
        [HttpGet]
        public ActionResult Hangman(GameState game)
        {
            HangmanGameState hangmanGame = new HangmanGameState(game);
            return View(nameof(Hangman), new HangmanGameState(hangmanGame));
        }

        [HttpGet]
        public ActionResult UserChose(GameState game)
        {
            return View(nameof(UserChose), game);
        }
        [HttpGet]
        public ActionResult WriteText(GameState game)
        {
            return View(nameof(WriteText), game);
        }
        [HttpGet]
        public ActionResult ChoseOption(GameState game)
        {
            return View(nameof(ChoseOption), game);
        }

        [HttpGet]
        public ActionResult GameEnd(bool isMulti = false)
        {
            return View(nameof(GameEnd), isMulti);
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