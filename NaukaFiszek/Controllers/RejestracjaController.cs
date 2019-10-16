using NaukaFiszek.Logic;
using DTO.Models;
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
                if (userDetails.Authorization.Login.Length < 3)
                {
                    ModelState.AddModelError($"{nameof(userDetails.Authorization)}.{nameof(userDetails.Authorization.Login)}", "Login jest zbyt krótki");
                }
                if (userDetails.Authorization.Password.Length < 8)
                {
                    ModelState.AddModelError($"{nameof(userDetails.Authorization)}.{nameof(userDetails.Authorization.Password)}", "Hasło jest zbyt krótkie");
                }
                else
                {
                    UserFiche user = UserFiche.Register(userDetails);
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError($"{nameof(userDetails.Authorization)}.{nameof(userDetails.Authorization.Login)}", "istnieje już urzytkownik o podanym loginie");
            }
            return View();
        }

        public ActionResult Wyloguj()
        {
            UserFiche.LogOut(Session);
            return RedirectToAction("Logowanie");
        }
        [HttpPost]
        public ActionResult Logowanie(AuthorizationDetails authorizationDetails)
        {
            UserFiche user = UserFiche.AutorizeUser(authorizationDetails);
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