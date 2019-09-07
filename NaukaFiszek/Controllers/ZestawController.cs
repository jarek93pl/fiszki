using Conector;
using NaukaFiszek.Filter;
using NaukaFiszek.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaukaFiszek.Controllers
{
    public class ZestawController : Controller
    {
        [FiszkiAutorize(IsAjaxRequest = true)]
        public ActionResult DodajZestaw()
        {
            return PartialView();
        }
        [FiszkiAutorize(IsAjaxRequest = true)]
        [HttpPost]
        public string DodajZestaw(string nameSet)
        {
            SetFiszka setFiszka = new SetFiszka();
            return setFiszka.AddSetFiszka(nameSet, UserFiszek.CurentUser.Id).ToString();
        }
        [FiszkiAutorize(IsAjaxRequest = true)]
        public ActionResult ListaZestawów()
        {
            SetFiszka setFiszka = new SetFiszka();
            return View(setFiszka.SearchSetsFiszka(UserFiszek.CurentUser.Id));
        }
        [HttpPost]
        public void Delete(int id)
        {
            SetFiszka setFiszka = new SetFiszka();
            setFiszka.Remove(id);
        }
    }
}