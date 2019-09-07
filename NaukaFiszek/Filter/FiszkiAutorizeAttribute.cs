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
        const string textDoWylogowania = "WylogowanieXDRwylogowanieXDR";
        public bool IsAjaxRequest { get; set; } 
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            UserFiche user = UserFiche.CurentUser;
            if (user == null)
            {
                if (IsAjaxRequest)
                {
                    ContentResult result = new ContentResult();
                    result.Content = textDoWylogowania;
                    filterContext.Result = result;
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Rejestracja/Logowanie");
                }
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }
    }
}