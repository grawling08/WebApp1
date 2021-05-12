using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class UsersController : Controller
    {
        private webapp_dbEntities db = new webapp_dbEntities();

        // GET: Users
        public ActionResult Index()
        {
            if (Session["idUser"] == "" || Session["idUser"] == null)
            {
                return RedirectToAction("Index","Home");
            }
            return View(db.users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["idUser"] == "" || Session["idUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password,fname,lname,address,dob,pic")] user user, HttpPostedFileBase file)
        {
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;
            string extension = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Images/"), _filename);

            user.pic = "~/Images/" + _filename;

            if(extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (ModelState.IsValid)
                {
                    db.users.Add(user);
                    db.SaveChanges();
                    file.SaveAs(path);
                    return RedirectToAction("Index");
                }
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["idUser"] == "" || Session["idUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,password,fname,lname,address,dob,pic")] user user, HttpPostedFileBase file)
        {
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;
            string extension = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Images/"), _filename);

            user.pic = "~/Images/" + _filename;

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    file.SaveAs(path);
                    return RedirectToAction("Index");
                }
            }
            
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["idUser"] == "" || Session["idUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
