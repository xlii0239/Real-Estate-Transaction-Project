using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment1.Models;
using Microsoft.AspNet.Identity;

namespace Assignment1.Controllers
{
    public class EventsController : Controller
    {
        private Entities db = new Entities();

        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        //// GET: Events/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Start,CustomerEmail")] Events events)
        {
            
            events.CustomerEmail = db.AspNetUsers.Find(User.Identity.GetUserId()).Email;
            //Get the current year month and day
            var currentYMD = events.Start.ToString("yyyy-MM-dd");
            if (ModelState.IsValid)
            {
                //Get all data from Events table
                var eventsList = db.Events.ToList();
                int dailyBooking = 0;
                //Count how many enevts in this day
                for (int i = 0; i < eventsList.Count(); i++) 
                {
                    if (eventsList[i].Start.ToString("yyyy-MM-dd").Equals(currentYMD))
                    {
                        dailyBooking++;
                    }
                }
                //The max booking accepted in one day is 5
                if (dailyBooking >= 5)
                {
                    return RedirectToAction("EventsFull");
                }

                db.Events.Add(events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(events);
        }

        // GET: Events/Create?date=YYYY-MM-DD
        public ActionResult Create(String date)
        {
            if (null == date) return RedirectToAction("Index");
            Events e = new Events();
            DateTime convertedDate = DateTime.Parse(date);
            e.Start = convertedDate;
            return View(e);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Start")] Events events)
        {
            if (ModelState.IsValid)
            {
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(events);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Events events = db.Events.Find(id);
            db.Events.Remove(events);
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

        public ActionResult DateExpired()
        {
            return View();
        }

        //Return view if events is full
        public ActionResult EventsFull()
        {
            return View();
        }
    }
}
