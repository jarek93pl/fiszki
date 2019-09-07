using NaukaFiszek.Logic;
using NaukaFiszek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class RejestracjaController : Controller
    {
        public ActionResult Logowanie()
        {
            return View();
        }
        public ActionResult Zarejestruj()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Zarejestruj(UserDetails userDetails)
        {
            try
            {
                UserFiszek user = UserFiszek.Register(userDetails);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Authorization.Login", "istnieje już urzytkownik o podanym loginie");
                return View();
            }


            return RedirectToAction("Index", "Home");
        }

        public ActionResult Wyloguj()
        {
            UserFiszek.LogOut(Session);
            return RedirectToAction("Logowanie");
        }
        [HttpPost]
        public ActionResult Logowanie(AuthorizationDetails authorizationDetails)
        {
            UserFiszek user = UserFiszek.AutorizeUser(authorizationDetails);
            if (user == null)
            {
                ModelState.AddModelError("Password", "błędne hasło");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}