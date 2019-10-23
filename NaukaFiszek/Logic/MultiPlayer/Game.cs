using DTO.Enums;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaukaFiszek.Logic.MultiPlayer
{
    public class Game
    {
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
            return CurentMultiPlayerGame.ListPlayer.Single(X => X.LoginToProcess== UserFiche.CurentUser.Name);
        }
        public void Register(UserFiche user)
        {
            ListPlayer.Register(new User(user));
            if (CurentMultiPlayerGame != null)
            {
                CurentMultiPlayerGame.Unregister(user);
            }
            CurentMultiPlayerGame = this;
        }
        public void Unregister(UserFiche user)
        {
            User gameUser = new User(user);
            if (CurentMultiPlayerGame != null)
            {
                CurentMultiPlayerGame.ListPlayer.Unregister(gameUser);
                CurentMultiPlayerGame = null;
            }
        }

        internal void Start()
        {
            throw new NotImplementedException();
        }
    }
}