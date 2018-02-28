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
    public class CarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Car
        [Authorize]
        public ActionResult Index(bool? id)
        {
            string ids = User.Identity.GetUserId();
            var carModels = db.CarModels.ToList();

            if (id != null)
                carModels = db.CarModels.Where(p => p.UserID == ids).ToList();

            foreach (var car in carModels)
                car.UserInfo = db.UserInfoModels.Where(p => p.ID == car.UserID).SingleOrDefault();

            return View(carModels);
        }

        // GET: Ads
        public ActionResult Ads()
        {
            //string ids = User.Identity.GetUserId();
            var carModels = db.CarModels.Where(p => p.Status == true).ToList();

            foreach (var car in carModels)
                car.UserInfo = db.UserInfoModels.Where(p => p.ID == car.UserID).SingleOrDefault();

            return View(carModels);
        }

        // GET: Wystaw
        [Authorize]
        public ActionResult Check(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.CarModels.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }
            string adr = "Edit/" + id.ToString();
            if (carModels.SalesAmount == null)
            {
                return RedirectToAction(adr);
            }

            carModels.Status = true;
            db.Entry(carModels).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Anuluj
        [Authorize]
        public ActionResult Checked(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.CarModels.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }

            carModels.Status = false;
            db.Entry(carModels).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Buy
        [Authorize]
        public ActionResult Buy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.CarModels.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }

            string ids = User.Identity.GetUserId();
            carModels.UserID = ids;

            carModels.PurchaseAmount = (double)carModels.SalesAmount;
            carModels.SalesAmount = null;
            carModels.Status = false;

            db.Entry(carModels).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Car/Details/5
   
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.CarModels.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }
            carModels.UserInfo = db.UserInfoModels.Where(p => p.ID == carModels.UserID).SingleOrDefault();
            
            carModels.Tuning = db.TuningModels.Where(p => p.CarID == id.ToString()).ToList();

            return View(carModels);
        }

        // GET: Car/Create
        [Authorize]
        public ActionResult Create()
        {
            var context = new ApplicationDbContext();
            string id = User.Identity.GetUserId();
            if (context.UserInfoModels.Where(u => u.ID == id).Count() > 0)
            {
                return View();
            }

            return RedirectToAction("../UserInfo/Create");
        }

        // POST: Car/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Mark,Model,Year,VIN,Name,Picture,PurchaseDate,PurchaseAmount,SalesAmount")] CarModels carModels)
        {
            carModels.UserID = User.Identity.GetUserId();
            carModels.Status = false;

            if (ModelState.IsValid)
            {
                db.CarModels.Add(carModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carModels);
        }

        // GET: Car/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.CarModels.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }
            return View(carModels);
        }

        // POST: Car/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Mark,Model,Year,VIN,Name,Picture,PurchaseDate,PurchaseAmount,SalesAmount")] CarModels carModels)
        {
            carModels.UserID = User.Identity.GetUserId();
            carModels.Status = false;

            if (ModelState.IsValid)
            {
                db.Entry(carModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carModels);
        }

        // GET: Car/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.CarModels.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }
            return View(carModels);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            CarModels carModels = db.CarModels.Find(id);
            db.CarModels.Remove(carModels);
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
