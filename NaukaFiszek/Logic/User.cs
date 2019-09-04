using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NaukaFiszek.Logic
{
    public class User
    {
        private User()
        {

        }
        public static User CurentUser(HttpSessionStateBase sessionStateBase)
        {
            return (User) sessionStateBase["NaukaFiszek.Logic.User.CurentUser"];
        }
       
    }
}