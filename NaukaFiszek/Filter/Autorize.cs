using NaukaFiszek.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace NaukaFiszek.Filter
{
    //ActionFilterAttribute, IAuthenticationFilter
    public class FiszkiAutorizeAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            User user = User.CurentUser(filterContext.HttpContext.Session);
            if (user == null)
            {
                filterContext.Result =new  RedirectResult("/Rejestracja/Logowanie");
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }
    }
}