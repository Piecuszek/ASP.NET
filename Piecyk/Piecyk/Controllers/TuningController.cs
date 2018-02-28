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
    public class TuningController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tuning
        [Authorize]
        public ActionResult Index()
        {
            string ids = User.Identity.GetUserId();
            //var tuningModels = db.TuningModels.Where(p => p.Car.UserID == ids).ToList();
            var tuningModels = db.TuningModels.ToList();

            foreach (var tun in tuningModels)
                tun.Car = db.CarModels.Where(p => p.ID.ToString() == tun.CarID).SingleOrDefault();

            var tuningModelss = tuningModels.Where(p => p.Car.UserID == ids).ToList();

            return View(tuningModelss);
        }

        // GET: Tuning/Create
        [Authorize]
        public ActionResult Create()
        {
            string ids = User.Identity.GetUserId();
            var carModels = db.CarModels.Where(p => p.UserID == ids).ToList();

            ViewBag.CarID = new SelectList(carModels, "ID", "Mark");

            return View();
        }

        // POST: Tuning/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,CarID,Name,Desc,Amount")] TuningModels tuningModels)
        {
            if (ModelState.IsValid)
            {
                db.TuningModels.Add(tuningModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(db.CarModels, "ID", "Mark", tuningModels.CarID);
            return View(tuningModels);
        }

        // GET: Tuning/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuningModels tuningModels = db.TuningModels.Find(id);
            if (tuningModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarID = new SelectList(db.CarModels, "ID", "Mark", tuningModels.CarID);
            return View(tuningModels);
        }

        // POST: Tuning/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarID,Name,Desc,Amount")] TuningModels tuningModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tuningModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(db.CarModels, "ID", "Mark", tuningModels.CarID);
            return View(tuningModels);
        }

        // GET: Tuning/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuningModels tuningModels = db.TuningModels.Find(id);
            if (tuningModels == null)
            {
                return HttpNotFound();
            }
            return View(tuningModels);
        }

        // POST: Tuning/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TuningModels tuningModels = db.TuningModels.Find(id);
            db.TuningModels.Remove(tuningModels);
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
