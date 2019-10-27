using DTO.Models;
using NaukaFiszek.Filter;
using NaukaFiszek.Logic;
using NaukaFiszek.Logic.MultiPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            bool gotLock = false;
            Guid guid = Guid.NewGuid();
            Game CurrentGame = null;
            if (ValidatingNewGameForPlayer(null))
            {
                try
                {
                    LockListGameDoesntStart.AcquireWriterLock(timeOut);
                    ListGameDoesntStart.Add(guid, new WeakReference<Game>(CurrentGame = new Game(multiPlayerGame) { IdentifyGuid = guid }));
                }
                catch
                {
                }
                finally
                {
                    gotLock = LockListGameDoesntStart.IsWriterLockHeld;
                    LockListGameDoesntStart.ReleaseWriterLock();
                }
            }
            else
            {
                return Json(new { GUID = (string)null });
            }
            if (gotLock)
            {
                CurrentGame.Register(UserFiche.CurentUser);
                Game.GetUserBySesionFicheUser().UserCanStart = true;
                return Json(new { GUID = guid.ToString() });
            }
            throw new NotSupportedException("Nie udało się stworzyć nowej gry");
        }

        [FiszkiAutorize(IsAjaxRequest = true)]
        [HttpGet]
        public ActionResult WaitingForPlayer()
        {
            WaitingForPlayerData waitingForPlayerData = new WaitingForPlayerData();
            Logic.MultiPlayer.User userPlayer;
            Game currentGame;
            if (Game.CurentMultiPlayerGame != null)
            {
                lock (this)
                {
                    userPlayer = Game.GetUserBySesionFicheUser();
                    currentGame = Game.CurentMultiPlayerGame;
                }

                if (currentGame != null)
                {
                    waitingForPlayerData = new WaitingForPlayerData()
                    {
                        GuidGame = currentGame.IdentifyGuid.ToString(),
                        UserCanStart = userPlayer.UserCanStart && !currentGame.IsStarted,
                        GameIsStated = currentGame.IsStarted,
                        GameIsEnd = currentGame.IsGameDeactivate,
                        PlayerResults = currentGame.ListPlayer.Select(X => new PlayerResult() { Name = X.LoginToProcess, Point = X.Point }).ToList()
                    };
                }
            }
            return View(waitingForPlayerData);
        }
        [HttpPost]
        public ActionResult WaitingForPlayer(WaitingForPlayerData data)
        {
            DTO.Models.ActionResultData result = null;
            try
            {
                LockListGameDoesntStart.AcquireWriterLock(timeOut);
                result = (DTO.Models.ActionResultData)Game.CurentMultiPlayerGame.Start();
                ListGameDoesntStart.Remove(new Guid(data.GuidGame));
            }
            catch
            {

            }
            finally
            {
                LockListGameDoesntStart.ReleaseWriterLock();
            }
            return Json(result);
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
                if (TryGetGameByGuid(waitingForPlayerData, out game) && ValidatingNewGameForPlayer(game))
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
        public bool ValidatingNewGameForPlayer(Game gameToJoined)
        {
            if (gameToJoined?.ListPlayer.Any(X => X.LoginToProcess == UserFiche.CurentUser.Name) == true)
            {
                return false;
            }
            if (Game.CurentMultiPlayerGame != null)
            {
                return false;
            }
            return true;
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
                List<PlayerResult> playerResults = new List<PlayerResult>();
                lock (Game.CurentMultiPlayerGame)
                {
                    playerResults.AddRange(Game.CurentMultiPlayerGame.ListPlayer.Select(X => new PlayerResult() { Name = X.LoginToProcess, Point = X.Point }));
                }
                playerResults.OrderByDescending(X => X.Point);
                returned = Extension.EventContentText(playerResults);
            }
            else
            {
                returned = GameEndComand;
            }
            return Content(returned, "text/event-stream");
        }
        [HttpGet]
        public ActionResult GetCommand()
        {
            string returned = string.Empty;
            Game game = Game.CurentMultiPlayerGame;
            if (game != null && game.IsGameDeactivate)
            {
                Game.CurentMultiPlayerGame = null;
                returned = GameEndComand;
            }
            else if (game != null && Game.GetUserBySesionFicheUser() != null)
            {
                lock (game)
                {
                    int LastIndex;
                    game.IsGameDeactivate |= game.LastCommandInGame;
                    var currentGameUser = Game.GetUserBySesionFicheUser();
                    if (currentGameUser.LastIndexComand != (LastIndex = game.CommandsMultiGame.Count))
                    {
                        var LastComand = game.CommandsMultiGame.Last();
                        string idFiche = ":0";
                        if (game.CommandsMultiGame.Last() == DTO.Enums.CommandMultiGame.ShowResponse)
                        {
                            var CurrentFiche = game.CurrentFiche;
                            idFiche = $":{currentGameUser.LastResponseDetails.IdFiche}:{(currentGameUser.LastResponseDetails.IsCorrect ? "1" : "0")}";
                        }
                        returned = $"data:{LastComand.ToString()}{idFiche}\n\n";
                        currentGameUser.LastIndexComand = LastIndex;
                    }
                }
            }
            else
            {
                returned = GameEndComand;
            }
            return Content(returned, "text/event-stream");
        }
        [HttpGet]
        public ActionResult GetFiche()
        {
            if (Game.CurentMultiPlayerGame != null)
            {
                Fiche fiche = null;
                MultiPlayerGameData multiPlayerGameData = null;
                GameState gameState = new GameState();
                gameState.IsMultiPlayer = true;
                lock (Game.CurentMultiPlayerGame)
                {
                    fiche = Game.CurentMultiPlayerGame.CurrentFiche;
                    multiPlayerGameData = Game.CurentMultiPlayerGame.MultiPlayerGameData;
                }
                gameState.Fiche = fiche;
                gameState.IntTypeAnswer = (int)multiPlayerGameData.TypeAnswer;
                gameState.LimitTimeSek = multiPlayerGameData.LimitTimeInSek;
                return multiPlayerGameData.TypeAnswer switch
                {
                    DTO.Enums.TypeAnswerMultiGame.WriteText => View($"../Gra/{nameof(GraController.WriteText)}", gameState),
                    DTO.Enums.TypeAnswerMultiGame.ChoseOption => View($"../Gra/{nameof(GraController.ChoseOption)}", gameState)
                };
            }
            throw new NotImplementedException("ta ścieżka w GetFiche jest nie opsugiwana");
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
                    Game.CurentMultiPlayerGame.Unregister();
                }
            }
        }

        [FiszkiAutorize(IsAjaxRequest = true)]
        [HttpPost]
        public void SendAnswer(SendAnswerRequest request)
        {
            if (Game.CurentMultiPlayerGame != null)
            {
                lock (Game.CurentMultiPlayerGame)
                {
                    Game.CurentMultiPlayerGame.SendAnswer(request.IdFiche, request.IsCorrect);
                }
            }
        }

        private static bool TryGetGameByGuid(WaitingForPlayerData waitingForPlayerData, out Game game)
        {
            game = null;
            return Guid.TryParse(waitingForPlayerData.GuidGame.Trim(), out Guid guid) &&
                 ListGameDoesntStart.TryGetValue(guid, out WeakReference<Game> gameWeak) &&
                 gameWeak.TryGetTarget(out game);
        }
    }
}