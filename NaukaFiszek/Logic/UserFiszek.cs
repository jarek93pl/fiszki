using NaukaFiszek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NaukaFiszek.Logic
{
    public class UserFiszek
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private UserFiszek()
        {

        }
        const string sessionName = "NaukaFiszek.Logic.User.CurentUser";
        public static UserFiszek CurentUser
        {
            get
            {
                return (UserFiszek)HttpContext.Current.Session[sessionName];
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

        public static UserFiszek AutorizeUser(AuthorizationDetails authorizationDetails)
        {

            using (Conector.User user = new Conector.User())
            {
                int id = user.Autorize(authorizationDetails.Login, Helper.Hash.SHA1Hash(authorizationDetails.Password));
                if (id != 0)
                {
                    UserFiszek userReturned = new UserFiszek();
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

        public static UserFiszek Register(UserDetails userDetails)
        {
            using (Conector.User user = new Conector.User())
            {
                user.AddUser(userDetails.Authorization.Login, Helper.Hash.SHA1Hash(userDetails.Authorization.Password));
            }
            return AutorizeUser(userDetails.Authorization);
        }
    }
}