using DTO.Enums;
using DTO.Models;
using NaukaFiszek.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NaukaFiszek.Logic.MultiPlayer
{
    public class Game
    {
        public List<CommandMultiGame> CommandsMultiGame { get; set; } = new List<CommandMultiGame>();
        public Fiche CurrentFiche { get; set; }

        DateTime CurrentFicheStartShow;

        Random random = new Random();
        List<Fiche> fichesInSet { get; set; } = new List<Fiche>();
        public bool IsGameDeactivate { get; internal set; }
        public bool IsStarted { get; set; }
        public ListPlayer<User> ListPlayer { get; set; } = new ListPlayer<User>();
        public MultiPlayerGameData MultiPlayerGameData { get; set; }
        public Guid IdentifyGuid { get; set; }
        public Game(MultiPlayerGameData multiPlayerGameData)
        {
            MultiPlayerGameData = multiPlayerGameData;
        }
        const string sessionName = "NaukaFiszek.Logic.MultiPlayerGame.CurentMultiPlayerGame";
        public static Game CurentMultiPlayerGame
        {
            get
            {
                return (Game)HttpContext.Current?.Session[sessionName];
            }
            set
            {
                HttpContext.Current.Session[sessionName] = value;
            }
        }

        public bool LastCommandInGame { get; private set; }

        public static User GetUserBySesionFicheUser()
        {
            return CurentMultiPlayerGame.ListPlayer.SingleOrDefault(X => X.LoginToProcess == UserFiche.CurentUser.Name);
        }
        public void Register(UserFiche user)
        {
            if (!ListPlayer.Any(X => X.LoginToProcess == user.Name))
            {
                if (CurentMultiPlayerGame != null)
                {
                    lock (CurentMultiPlayerGame)
                    {
                        CurentMultiPlayerGame.Unregister(user);
                    }
                }
                ListPlayer.Register(new User(user));
                CurentMultiPlayerGame = this;
            }
        }
        public void Unregister(UserFiche user = null)
        {
            User gameUser;
            if (user == null)
            {
                gameUser = GetUserBySesionFicheUser();
            }
            else
            {
                gameUser = new User(user);
            }
            if (CurentMultiPlayerGame != null && gameUser != null)
            {
                CurentMultiPlayerGame.ListPlayer.Unregister(gameUser);
                if (CurrentFiche != null && ListPlayer.Any())
                {
                    TryNewFiche();
                }
                CurentMultiPlayerGame = null;
            }
        }

        internal bool Start()
        {
            IsStarted = true;
            fichesInSet = new List<Fiche>();
            using (Conector.Fiche fiche = new Conector.Fiche())
            {
                fichesInSet.AddRange(fiche.SearchFiches(MultiPlayerGameData.IdSetFiche));
            }
            if (LoadFiche())
            {
                ShowFiche();
                return true;
            }
            return false;
        }

        bool LoadFiche()
        {
            CurrentFicheStartShow = default(DateTime);
            bool DontFind = true;
            while (DontFind)
            {
                if (fichesInSet.Count == 0)
                {

                    LastCommandInGame = true;
                    return false;
                }
                using (Conector.Fiche conector = new Conector.Fiche())
                {
                    var TmpCurrentFiche = conector.LoadFiche(fichesInSet[random.Next(0, fichesInSet.Count)].Id);
                    fichesInSet.Remove(TmpCurrentFiche);
                    DontFind = !((TypeAnswer)MultiPlayerGameData.TypeAnswer).Can(TmpCurrentFiche);
                    if (DontFind)
                    {
                        CurrentFiche = null;
                    }
                    else
                    {
                        CurrentFiche = TmpCurrentFiche;
                    }
                }
            }
            return true;
        }
        private void MoveAfrerEndFiche()
        {
            const int TimeShowingPlayerListMs = 5000;
            const int TimeShowingResponse = 9000;
            if (!LoadFiche())
            {
                CommandAdd(CommandMultiGame.ShowList);
            }
            else
            {
                CommandAdd(CommandMultiGame.ShowResponse);
                Task.Run(async () =>
                {
                    await Task.Delay(TimeShowingResponse);
                    lock (this)
                    {
                        CommandAdd(CommandMultiGame.ShowList);
                    }
                    await Task.Delay(TimeShowingPlayerListMs);
                    lock (this)
                    {
                        ShowFiche();
                    }
                }
                );
            }
        }

        private void ShowFiche()
        {
            if (MultiPlayerGameData.IsLimitTime)
            {
                var CurrentFicheId = CurrentFiche.Id;
                Task.Run(async () =>
                {
                    await Task.Delay(MultiPlayerGameData.LimitTimeInSek * 1000 + 3000);
                    lock (this)
                    {
                        if (CurrentFicheId == CurrentFiche.Id)
                        {
                            foreach (var item in ListPlayer.Where(X => !X.AnswersByFicheId.ContainsKey(CurrentFiche.Id)))
                            {
                                item.AnswersByFicheId.Add(CurrentFiche.Id, LoadFicheResponse(false));
                            }
                        }
                        TryNewFiche();
                    }
                });
            }
            CurrentFicheStartShow = DateTime.Now;
            CommandAdd(CommandMultiGame.LoadNextFiche);
        }

        void CommandAdd(CommandMultiGame commandMultiGame)
        {
            CommandsMultiGame.Add(commandMultiGame);
        }
        ~Game()
        {
            try
            {
                MultiPlayerController.LockListGameDoesntStart.AcquireWriterLock(5000);
            }
            catch (Exception ex)
            {
                MultiPlayerController.ListGameDoesntStart.Remove(IdentifyGuid);
            }
            finally
            {
                MultiPlayerController.LockListGameDoesntStart.ReleaseWriterLock();
            }
        }

        internal void SendAnswer(int idFiche, bool isCorrect)
        {
            var userGame = GetUserBySesionFicheUser();
            if (CurrentFiche.Id == idFiche && !userGame.AnswersByFicheId.ContainsKey(idFiche))
            {
                ResponseDetails response = LoadFicheResponse(isCorrect);
                userGame.AnswersByFicheId.Add(idFiche, response);
                TryNewFiche();
            };
        }

        private ResponseDetails LoadFicheResponse(bool isCorrect)
        {
            return new ResponseDetails()
            {
                IdFiche = CurrentFiche.Id,
                IsCorrect = isCorrect,
                TimeAnsweringMilisecond = (int)(DateTime.Now - CurrentFicheStartShow).TotalMilliseconds
            };
        }

        private void TryNewFiche()
        {
            if (ListPlayer.All(X => X.AnswersByFicheId.ContainsKey(CurrentFiche.Id)))
            {
                CountPointPlayer();
                MoveAfrerEndFiche();
            }
        }

        private void CountPointPlayer()
        {
            const float PointInMove = 1000f;
            var UserAndLastResponseIfCorrect = ListPlayer
                .Select(X => (User: X, LastResponse: X.AnswersByFicheId[CurrentFiche.Id]))
                .Where(X => X.LastResponse.IsCorrect).ToList();
            float InverseSum = UserAndLastResponseIfCorrect.Sum(X => 1f / X.LastResponse.TimeAnsweringMilisecond);
            foreach (var item in UserAndLastResponseIfCorrect)
            {
                item.User.Point += (int)(((1f / item.LastResponse.TimeAnsweringMilisecond)) * PointInMove / InverseSum);
            }
        }
    }
}
