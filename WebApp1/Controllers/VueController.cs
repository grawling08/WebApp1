using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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

            var _users = db.users.Add(json);
            db.SaveChanges();

            return Json(_users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditUser(user json)
        {

            var _users = db.Entry(json).State = EntityState.Modified;
            db.SaveChanges();
            return Json(_users, JsonRequestBehavior.AllowGet);

        }

        
    }
}