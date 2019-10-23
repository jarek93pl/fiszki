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
                if (!ValidateAuthorization($"{nameof(userDetails.Authorization)}.", userDetails.Authorization))
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
        public bool ValidateAuthorization(string prefix, AuthorizationDetails authorizationDetails)
        {
            bool IsNotValid = false;
            if (authorizationDetails.Login == null || authorizationDetails.Login.Length < 3)
            {
                IsNotValid |= true;
                ModelState.AddModelError($"{prefix}{nameof(AuthorizationDetails.Login)}", "Login jest zbyt krótki");
            }
            if (authorizationDetails.Password == null || authorizationDetails.Password.Length < 8)
            {
                IsNotValid |= true;
                ModelState.AddModelError($"{prefix}{nameof(AuthorizationDetails.Password)}", "Hasło jest zbyt krótkie");
            }
            return IsNotValid;
        }
        public ActionResult Wyloguj()
        {
            UserFiche.LogOut(Session);
            return RedirectToAction("Logowanie");
        }
        [HttpPost]
        public ActionResult Logowanie(AuthorizationDetails authorizationDetails)
        {
            if (ValidateAuthorization("",authorizationDetails))
            {
                return View();
            }
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