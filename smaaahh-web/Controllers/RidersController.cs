using smaaahh_dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace smaaahh_web.Controllers
{
    public class RidersController : Controller
    {
        private Db db = new Db();

        public Rider rider { get; private set; }

        // GET: rider
        public ActionResult Index()
        {
            return View(db.Riders.ToList());
        }

        // GET: rider/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rider rider  = db.Riders.Find(id);
            if (rider == null)
            {
                return HttpNotFound();
            }
            return View(rider);
        }

        // GET: rider/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Driver/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "RiderId, Rating,PosX,PosY")] Rider rider )
        //{


        //    return View(rider);
        //}

        // GET: Rider/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rider rider = db.Riders.Find(id);
            if (rider == null)
            {
                return HttpNotFound();
            }


            return View(rider);
        }

        // POST: Rider/Edit/id
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RiderId, Rating,PosX,PosY")] Rider rider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rider);
        }

        // GET: rider/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Rider/Delete/5
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Driver driver = db.Drivers.Find(id);
            db.Riders.Remove(rider);
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
