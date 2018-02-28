using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Piecyk.Models;
using Microsoft.AspNet.Identity;

namespace Piecyk.Controllers
{
    public class UserInfoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserInfo
        [Authorize]
        public ActionResult Index()
        {
            return View(db.UserInfoModels.ToList());
        }

        // GET: UserInfo/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfoModels userInfoModels = db.UserInfoModels.Find(id);
            if (userInfoModels == null)
            {
                return HttpNotFound();
            }

            var carModels = db.CarModels.Where(p => p.UserID == id).ToList();

            userInfoModels.Car = carModels;

            return View(userInfoModels);
        }

        // GET: UserInfo/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserInfo/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "FirstName,LastName,PhoneNo")] UserInfoModels userInfoModels)
        {
            userInfoModels.ID = User.Identity.GetUserId();
            userInfoModels.Email = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.UserInfoModels.Add(userInfoModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userInfoModels);
        }

        // GET: UserInfo/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfoModels userInfoModels = db.UserInfoModels.Find(id);
            if (userInfoModels == null)
            {
                return HttpNotFound();
            }
            if (id != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            return View(userInfoModels);
        }

        // POST: UserInfo/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,PhoneNo")] UserInfoModels userInfoModels)
        {
            userInfoModels.ID = User.Identity.GetUserId();
            userInfoModels.Email = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Entry(userInfoModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userInfoModels);
        }

        // GET: UserInfo/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfoModels userInfoModels = db.UserInfoModels.Find(id);
            if (userInfoModels == null)
            {
                return HttpNotFound();
            }
            return View(userInfoModels);
        }

        // POST: UserInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(string id)
        {
            UserInfoModels userInfoModels = db.UserInfoModels.Find(id);
            db.UserInfoModels.Remove(userInfoModels);
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
