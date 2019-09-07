using NaukaFiszek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NaukaFiszek.Logic
{
    public class UserFiche
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private UserFiche()
        {

        }
        const string sessionName = "NaukaFiszek.Logic.User.CurentUser";
        public static UserFiche CurentUser
        {
            get
            {
                return (UserFiche)HttpContext.Current.Session[sessionName];
            }
            set
            {
                HttpContext.Current.Session[sessionName] = value;
            }
        }

        internal static void LogOut(HttpSessionStateBase session)
        {
            session[sessionName] = null;
        }

        public static UserFiche AutorizeUser(AuthorizationDetails authorizationDetails)
        {

            using (Conector.User user = new Conector.User())
            {
                int id = user.Autorize(authorizationDetails.Login, Helper.Hash.SHA1Hash(authorizationDetails.Password));
                if (id != 0)
                {
                    UserFiche userReturned = new UserFiche();
                    CurentUser = userReturned;
                    userReturned.Id = id;
                    return userReturned;
                }
                else
                {
                    return null;
                }
            }


        }

        public static UserFiche Register(UserDetails userDetails)
        {
            using (Conector.User user = new Conector.User())
            {
                user.AddUser(userDetails.Authorization.Login, Helper.Hash.SHA1Hash(userDetails.Authorization.Password));
            }
            return AutorizeUser(userDetails.Authorization);
        }
    }
}