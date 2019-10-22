using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaukaFiszek.Logic.MultiPlayer
{
    public class User
    {
        private string SetedLogin { get; set; }
        public User()
        {

        }
        public User(string login)
        {
            SetedLogin = login;
        }
        public User(Logic.UserFiche userFiche)
        {
            this.UserFiche = userFiche;
        }
        public string LoginToProcess { get => UserFiche?.Name ?? SetedLogin; }
        Logic.UserFiche UserFiche { get; }
        public int Point = 0;
        public override bool Equals(object obj)
        {
            if (obj is User userB)
            {
                return userB.LoginToProcess == LoginToProcess;
            }
            return false;
        }
        public override int GetHashCode() => LoginToProcess.GetHashCode();
        public int playerEndIndex { get; set; }
        public override string ToString()
        {
            return LoginToProcess;
        }
    }
}