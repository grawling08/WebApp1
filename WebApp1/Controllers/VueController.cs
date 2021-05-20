using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class VueController : Controller
    {
        private webapp_dbEntities db = new webapp_dbEntities();
        // GET: Vue
        public ActionResult Index()
        {
            return View();
        }
        public String GetUsers()
        {
            var _users = db.users.ToList();
            
            return JsonConvert.SerializeObject(_users, Formatting.Indented);
        }

        [HttpPost]
        public JsonResult SaveUsers(user json)
        {
            //var _users = id;
            json.password = HomeController.GetMD5(json.password);
            var _users = db.users.Add(json);
            db.SaveChanges();

            return Json(_users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditUser(user json)
        {
            json.password = HomeController.GetMD5(json.password);
            var _users = db.Entry(json).State = EntityState.Modified;
            db.SaveChanges();

            return Json(_users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            user user = db.users.Find(id);
            var _users = db.users.Remove(user);
            db.SaveChanges();

            return Json(_users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UploadImg(HttpPostedFileBase file, int id)
        {
            HttpContext context = System.Web.HttpContext.Current;
            HttpPostedFile postedFile = context.Request.Files.Count > 0 ? context.Request.Files.Get(0) : null;

            string filename = Path.GetFileName(postedFile.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename.Replace(" ", String.Empty);
            string extension = Path.GetExtension(postedFile.FileName);
            string path = Path.Combine(Server.MapPath("~/Images/"), _filename);

            user user = db.users.Find(id);
            user.pic = "~/Images/" + _filename.ToString();
            var _users = db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            postedFile.SaveAs(path);

            return Json(_users, JsonRequestBehavior.AllowGet);
        }

        
    }
}