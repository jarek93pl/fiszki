﻿using DTO.Models;
using NaukaFiszek.Filter;
using NaukaFiszek.Logic;
using NaukaFiszek.Logic.MultiPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NaukaFiszek.Controllers
{
    public class MultiPlayerController : Controller
    {
        const int timeOut = 2000;
        public static ReaderWriterLock LockListGameDoesntStart = new ReaderWriterLock();
        public readonly static Dictionary<Guid, WeakReference<Game>> ListGameDoesntStart = new Dictionary<Guid, WeakReference<Game>>();

        [FiszkiAutorize(IsAjaxRequest = true)]
        [HttpGet]
        public ActionResult CreateGame()
        {
            //UserFiche.AutorizeUser(new AuthorizationDetails() { Login = "jarektest", Password = "haslohaslo" });
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
                Game CurrentGame;
                ListGameDoesntStart.Add(guid, new WeakReference<Game>(CurrentGame = new Game(multiPlayerGame) { IdentifyGuid = guid }));
                CurrentGame.Register(UserFiche.CurentUser);
                Game.GetUserBySesionFicheUser().UserCanStart = true;
                return Json(new { GUID = guid.ToString() });
            }
            catch
            {

            }
            finally
            {
                LockListGameDoesntStart.ReleaseWriterLock();
            }
            throw new NotSupportedException("Nie udało się stworzyć nowej gry");
        }

        [FiszkiAutorize(IsAjaxRequest = true)]
        [HttpGet]
        public ActionResult WaitingForPlayer()
        {
            WaitingForPlayerData waitingForPlayerData = new WaitingForPlayerData()
            {
                GuidGame = Game.CurentMultiPlayerGame.IdentifyGuid.ToString(),
                UserCanStart = Game.GetUserBySesionFicheUser().UserCanStart
            };
            return View(waitingForPlayerData);
        }
        [HttpPost]
        public void WaitingForPlayer(WaitingForPlayerData data)
        {
            try
            {
                LockListGameDoesntStart.AcquireWriterLock(timeOut);
                Game.CurentMultiPlayerGame.Start();
                ListGameDoesntStart.Remove(new Guid(data.GuidGame));
            }
            catch
            {

            }
            finally
            {
                LockListGameDoesntStart.ReleaseWriterLock();
            }
        }
        [FiszkiAutorize(IsAjaxRequest = true)]
        [HttpGet]
        public ActionResult JoinToGame()
        {
            return View();
        }
        [HttpPost]
        public ActionResult JoinToGame(WaitingForPlayerData waitingForPlayerData)
        {
            WaitingForPlayerData returned = new WaitingForPlayerData();
            try
            {
                LockListGameDoesntStart.AcquireReaderLock(timeOut);
                Game game;
                if (TryGetGameByGuid(waitingForPlayerData, out game))
                {
                    if (game != null)
                    {
                        lock (game)
                        {
                            game.Register(UserFiche.CurentUser);
                            returned.GuidGame = game.IdentifyGuid.ToString();
                        }
                    }
                }
            }
            catch
            {

            }
            finally
            {
                LockListGameDoesntStart.ReleaseReaderLock();
            }
            return Json(returned);
        }

        private static bool TryGetGameByGuid(WaitingForPlayerData waitingForPlayerData, out Game game)
        {
            game = null;
            return Guid.TryParse(waitingForPlayerData.GuidGame.Trim(), out Guid guid) &&
                 ListGameDoesntStart.TryGetValue(guid, out WeakReference<Game> gameWeak) &&
                 gameWeak.TryGetTarget(out game);
        }

        [HttpGet]
        public ActionResult ListPlayer()
        {
            return View();
        }

        const string GameEndComand = "data:break\n\n";
        [HttpGet]
        public ActionResult RefreshListPlayer()
        {
            string returned = string.Empty;
            if (Game.CurentMultiPlayerGame != null && Game.CurentMultiPlayerGame.IsGameDeactivate)
            {
                Game.CurentMultiPlayerGame = null;
                returned = GameEndComand;
            }
            else if (Game.CurentMultiPlayerGame != null && Game.GetUserBySesionFicheUser() != null)
            {
                lock (Game.CurentMultiPlayerGame)
                {
                    var currentGameUser = Game.GetUserBySesionFicheUser();
                    var logBackend = Game.CurentMultiPlayerGame.ListPlayer.ChangeLogs(currentGameUser.playerEndIndex);
                    currentGameUser.playerEndIndex = logBackend.EndIndex;
                    if (logBackend.ChangeLogs.Any())
                    {
                        ChangeLog<PlayerDetails> returnedObject = new ChangeLog<PlayerDetails>()
                        {
                            EndIndex = logBackend.EndIndex,
                            ChangeLogs = logBackend.ChangeLogs.Select(
                                X => new PlayerDetails() { Login = X.Login.LoginToProcess, ActionName = X.Status.ToString(), Point = X.Login.Point }).ToList()
                        };
                        returned = Extension.EventContentText(returnedObject);
                    }
                }
            }
            else
            {
                returned = GameEndComand;
            }
            return Content(returned, "text/event-stream");
        }


        [NaukaFiszek.Filter.FiszkiAutorize(IsAjaxRequest = true)]
        [HttpPost]
        public ActionResult Register(string guidString)
        {
            Guid guid = Guid.Parse(guidString);
            try
            {
                LockListGameDoesntStart.AcquireReaderLock(timeOut);
                var game = ListGameDoesntStart[guid];
            }
            catch
            {

            }
            finally
            {
                LockListGameDoesntStart.ReleaseReaderLock();
            }

            throw new NotSupportedException("Nie udało się zarejestrować do gry");
        }

        [NaukaFiszek.Filter.FiszkiAutorize(IsAjaxRequest = true)]
        [HttpPost]
        public void Unregister()
        {
            if (Game.CurentMultiPlayerGame != null)
            {
                if (Game.GetUserBySesionFicheUser().UserCanStart && !Game.CurentMultiPlayerGame.IsStarted)
                {
                    Game.CurentMultiPlayerGame.IsGameDeactivate = true;
                    try
                    {
                        MultiPlayerController.LockListGameDoesntStart.AcquireWriterLock(5000);
                    }
                    catch (Exception ex)
                    {
                        MultiPlayerController.ListGameDoesntStart.Remove(Game.CurentMultiPlayerGame.IdentifyGuid);
                    }
                    finally
                    {
                        MultiPlayerController.LockListGameDoesntStart.ReleaseWriterLock();
                    }
                }
                lock (Game.CurentMultiPlayerGame)
                {
                    Game.CurentMultiPlayerGame.Unregister(UserFiche.CurentUser);
                }
            }
        }
    }
}