using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
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
            var _users = db.users.SqlQuery("SELECT TOP (2000) [id] ,[username] ,[password] ,[fname] ,[lname] ,[address] ,[dob] ,[pic] FROM [dbo].[users]");
            
            return JsonConvert.SerializeObject(_users, Formatting.Indented);
        }
        
    }
}