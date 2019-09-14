using NaukaFiszek.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace NaukaFiszek.Controllers
{
    public class ComonController : Controller
    {
        // GET: Comon
        public ActionResult Index()
        {
            return View();
        }
        public static string PathContent
        {
            get
            {
                return ConfigurationManager.AppSettings["pathfile"];
            }
        }
        [HttpPost]
        public ActionResult SaveFile(FileData fileData)
        {
            using (Conector.Common common = new Conector.Common())
            {
                int FileId = common.SaveFile(fileData.Extension);
                global::System.IO.File.WriteAllBytes(Path.Combine(PathContent, FileId.ToString()), fileData.DataFile);
            }
            return View();
        }
    }
}