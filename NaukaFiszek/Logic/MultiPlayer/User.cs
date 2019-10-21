using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaukaFiszek.Logic.MultiPlayer
{
    public class User
    {
        public User(Logic.UserFiche userFiche)
        {
            this.UserFiche = userFiche;
        }
        public Logic.UserFiche UserFiche { get;  }
        public int Point = 0;
    }
}