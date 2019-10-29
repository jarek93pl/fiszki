using DTO.Models;
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
        static bool IsLoaded = false;

        void LoadFolder()
        {
            if (!IsLoaded)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Server.MapPath(ConfigurationManager.AppSettings["pathfile"]));
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
                IsLoaded = true;
            }
        }
        // GET: Comon
        public ActionResult Index()
        {
            return View();
        }
        public string PathContent
        {
            get
            {
                LoadFolder();
                return Server.MapPath(ConfigurationManager.AppSettings["pathfile"]);
            }
        }
        [HttpPost]
        public JsonResult SaveFile()
        {

            HttpPostedFileBase file = Request.Files[0];
            int FileId;
            using (Conector.Comon common = new Conector.Comon())
            {
                FileId = common.SaveFile(Path.GetExtension(file.FileName));

                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                byte[] data = target.ToArray();

                global::System.IO.File.WriteAllBytes(Path.Combine(PathContent, FileId.ToString()), data);
            }
            return Json(new SaveFileResponse() { Id = FileId });
        }



        public ActionResult LoadFile(int id)
        {
            string extension = null;
            using (Conector.Comon common = new Conector.Comon())
            {
                extension = common.GetExtension(id);
            }
            return File(global::System.IO.File.ReadAllBytes(Path.Combine(PathContent, id.ToString())), extension);
        }
        public ActionResult DontFindSet()
        {
            return View(nameof(DontFindSet));
        }

    }
}