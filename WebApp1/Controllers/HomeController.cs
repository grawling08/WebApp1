using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class HomeController : Controller
    {
        private webapp_dbEntities db = new webapp_dbEntities();

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Login()
        {
            if (TempData["errMsg"] != null)
            {
                ViewBag.error = TempData["errMsg"];
                TempData.Remove("errMsg");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.users.Where(s => s.username.Equals(username)).ToList();
                if (data.Count() > 0)
                {
                    //check password
                    if (data.FirstOrDefault().password.ToString() != f_password)
                    {
                        TempData["errMsg"] = "Login failed. Incorrect Password";
                        return RedirectToAction("Login");
                    } 
                    //add session
                    Session["FullName"] = data.FirstOrDefault().fname + " " + data.FirstOrDefault().lname;
                    Session["idUser"] = data.FirstOrDefault().id;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errMsg"] = "Login failed. No Registered User";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}