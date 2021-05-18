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
            var _users = db.users.SqlQuery("SELECT TOP (2000) [id] ,[username] ,[password] ,[fname] ,[lname] ,[address] ,[dob] ,[pic] FROM [dbo].[users]");
            
            return JsonConvert.SerializeObject(_users, Formatting.Indented);
        }

        [HttpPost]
        public JsonResult SaveUsers(user json)
        {
            Console.WriteLine("The posted data is: " + json);
            
            //string qry = "INSERT INTO users (username, password, fname, lname, address, dob, pic) VALUES(@username,@password,@fname,@lname,@address,@dob,@pic)";
            //SqlParameter _username = new SqlParameter("username", json.username);
            //SqlParameter _password = new SqlParameter("password", json.password);
            //SqlParameter _fname = new SqlParameter("fname", json.fname);
            //SqlParameter _lname = new SqlParameter("lname", json.lname);
            //SqlParameter _address = new SqlParameter("address", json.address);
            //SqlParameter _dob = new SqlParameter("dob", json.dob);
            //SqlParameter _pic = new SqlParameter("pic", json.pic);
            //object[] parameters = new object[] { _username, _password, _fname, _lname, _address, _dob, _pic };

            //var _users = db.users.SqlQuery(qry, parameters);
            var _users = db.users.Add(json);
            db.SaveChanges();

            return Json(_users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditUser(user json)
        {
            //string qry = "UPDATE users username = @username, password = @password, fname = @fname, lname = @lname, address = @address, dob = @dob, pic = @pic WHERE id = @id";
            //SqlParameter _username = new SqlParameter("username", json.username);
            //SqlParameter _password = new SqlParameter("password", json.password);
            //SqlParameter _fname = new SqlParameter("fname", json.fname);
            //SqlParameter _lname = new SqlParameter("lname", json.lname);
            //SqlParameter _address = new SqlParameter("address", json.address);
            //SqlParameter _dob = new SqlParameter("dob", json.dob);
            //SqlParameter _pic = new SqlParameter("pic", json.pic);
            //SqlParameter _id = new SqlParameter("id", json.id);

            //object[] parameters = new object[] { _username, _password, _fname, _lname, _address, _dob, _pic, _id };

            //var _users = db.users.SqlQuery(qry, parameters);
            //if (json != null)
            //{
                db.Entry(json).State = EntityState.Modified;
                //var _users = db.users.Attach(json);
                var _users = db.SaveChanges();
                //Console.WriteLine("The posted data is: " + json);
                return Json(_users, JsonRequestBehavior.AllowGet);
            //}

            //return Json(json, JsonRequestBehavior.AllowGet);
        }

        
    }
}