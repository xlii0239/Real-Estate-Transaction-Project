using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class HouseSetsController : Controller
    {
        private Entities db = new Entities();

        // GET: HouseSets
        public ActionResult Index()
        {
            return View(db.HouseSet.ToList());
        }

        // GET: HouseSets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseSet houseSet = db.HouseSet.Find(id);
            if (houseSet == null)
            {
                return HttpNotFound();
            }
            return View(houseSet);
        }

        // GET: HouseSets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HouseSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Address,Price,Latitude,Longitude,Bedroom,Bathroom,Parking,Area")] HouseSet houseSet)
        {
            if (ModelState.IsValid)
            {
                db.HouseSet.Add(houseSet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(houseSet);
        }

        // GET: HouseSets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseSet houseSet = db.HouseSet.Find(id);
            if (houseSet == null)
            {
                return HttpNotFound();
            }
            return View(houseSet);
        }

        // POST: HouseSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Address,Price,Latitude,Longitude,Bedroom,Bathroom,Parking,Area")] HouseSet houseSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(houseSet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(houseSet);
        }

        // GET: HouseSets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseSet houseSet = db.HouseSet.Find(id);
            if (houseSet == null)
            {
                return HttpNotFound();
            }
            return View(houseSet);
        }

        // POST: HouseSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Try to delete a house, if it has umcomplete transaction, them redirect to HouseHasTransaction pageq
            try
            {
                HouseSet houseSet = db.HouseSet.Find(id);
                db.HouseSet.Remove(houseSet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(DbUpdateException e)
            {
                return RedirectToAction("HouseHasTransaction");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult HouseHasTransaction()
        {
            return View();
        }
    }
}
