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
                return (Game)HttpContext.Current.Session[sessionName];
            }
            set
            {
                HttpContext.Current.Session[sessionName] = value;
            }
        }
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
                    CurentMultiPlayerGame.Unregister(user);
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
            if (CurentMultiPlayerGame != null)
            {
                CurentMultiPlayerGame.ListPlayer.Unregister(gameUser);
                CurentMultiPlayerGame = null;
            }
        }

        internal void Start()
        {
            IsStarted = true;
            fichesInSet = new List<Fiche>();
            using (Conector.Fiche fiche = new Conector.Fiche())
            {
                fichesInSet.AddRange(fiche.SearchFiches(MultiPlayerGameData.IdSetFiche));
            }
            LoadFiche();
        }

        void LoadFiche()
        {
            CurrentFiche = null;
            CurrentFicheStartShow = default(DateTime);
            bool DontFind = true;
            while (DontFind)
            {
                if (fichesInSet.Count == 0)
                {
                    MoveAfrerEndFiche(true);
                    break;
                }
                using (Conector.Fiche conector = new Conector.Fiche())
                {
                    var TmpCurrentFiche = conector.LoadFiche(fichesInSet[random.Next(0, fichesInSet.Count)].Id);
                    fichesInSet.Remove(TmpCurrentFiche);
                    DontFind = !((TypeAnswer)MultiPlayerGameData.TypeAnswer).Can(TmpCurrentFiche);
                    if (!DontFind)
                    {
                        ShowFiche(TmpCurrentFiche);
                    }
                }
            }
        }
        private void MoveAfrerEndFiche(bool IsGameDeactivate)
        {
            const int TimeShowingPlayerListMs = 5000;
            const int TimeShowingResponse = 5000;
            if (IsGameDeactivate)
            {
                this.IsGameDeactivate = IsGameDeactivate;
                CommandAdd(CommandMultiGame.ShowList);
            }
            else
            {
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
                        LoadFiche();
                    }
                }
                );
                CommandAdd(CommandMultiGame.ShowResponse);
            }
        }

        private void ShowFiche(Fiche TmpCurrentFiche)
        {
            CurrentFiche = TmpCurrentFiche;
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
                ResponseDetails response = new ResponseDetails()
                {
                    IdFiche = idFiche,
                    IsCorrect = isCorrect,
                    TimeAnsweringMilisecond = (int)(DateTime.Now - CurrentFicheStartShow).TotalMilliseconds
                };
                userGame.AnswersByFicheId.Add(idFiche, response);

                if (ListPlayer.All(X => X.AnswersByFicheId.ContainsKey(idFiche)))
                {
                    CountPointPlayer();
                    MoveAfrerEndFiche(false);
                }
            };
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
