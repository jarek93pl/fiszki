using DTO.Models;
using NaukaFiszek.Filter;
using NaukaFiszek.Logic;
using NaukaFiszek.Logic.MultiPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class MultiPlayerController : Controller
    {
        const int timeOut = 2000;
        static ReaderWriterLock LockListGameDoesntStart = new ReaderWriterLock();
        public readonly static Dictionary<Guid, Game> ListGameDoesntStart = new Dictionary<Guid, Game>();

        [FiszkiAutorize(IsAjaxRequest = true)]
        [HttpGet]
        public ActionResult CreateGame()
        {
            UserFiche.AutorizeUser(new AuthorizationDetails() { Login = "jarektest", Password = "haslohaslo" });
            MultiPlayerGameData multiPlayerGame = new MultiPlayerGameData();
            using Conector.SetFiche setFicheConector = new Conector.SetFiche();
            if (!setFicheConector.AnySetsFicheExist(UserFiche.CurentUser.Id))
            {
                return RedirectToAction(nameof(ComonController.DontFindSet), "Comon");
            }
            multiPlayerGame.AvailableSetFiches = setFicheConector.SearchSetsFiche(UserFiche.CurentUser.Id, null);
            return View(multiPlayerGame);
        }

        [NaukaFiszek.Filter.FiszkiAutorize(IsAjaxRequest = true)]
        [HttpPost]
        public ActionResult CreateGame(MultiPlayerGameData multiPlayerGame)
        {
            try
            {
                LockListGameDoesntStart.AcquireWriterLock(timeOut);
                Guid guid = Guid.NewGuid();
                ListGameDoesntStart.Add(guid, new Game(multiPlayerGame));
                return Json(new { GUID = guid.ToString() });
            }
            catch (Exception ex)
            {
            }
            finally
            {
                LockListGameDoesntStart.ReleaseWriterLock();
            }
            throw new NotSupportedException("Nie udało się stworzyć nowej gry");
        }

        [NaukaFiszek.Filter.FiszkiAutorize(IsAjaxRequest = true)]
        [HttpGet]
        public ActionResult Register(string guidString)
        {
            Guid guid = Guid.Parse(guidString);
            try
            {
                LockListGameDoesntStart.AcquireReaderLock(timeOut);
            }
            catch (Exception ex)
            {
                var game = ListGameDoesntStart[guid];
            }
            finally
            {
                LockListGameDoesntStart.ReleaseReaderLock();
            }

            throw new NotSupportedException("Nie udało się zarejestrować do gry");
        }

    }
}