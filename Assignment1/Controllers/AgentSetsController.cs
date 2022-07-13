using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class AgentSetsController : Controller
    {
        private Entities db = new Entities();

        // GET: AgentSets
        public ActionResult Index()
        {
            return View(db.AgentSet.ToList());
        }

        // GET: AgentSets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentSet agentSet = db.AgentSet.Find(id);
            if (agentSet == null)
            {
                return HttpNotFound();
            }
            return View(agentSet);
        }

        // GET: AgentSets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgentSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PhoneNumber,Email,AgentName")] AgentSet agentSet)
        {
            if (ModelState.IsValid)
            {
                db.AgentSet.Add(agentSet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agentSet);
        }

        // GET: AgentSets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentSet agentSet = db.AgentSet.Find(id);
            if (agentSet == null)
            {
                return HttpNotFound();
            }
            return View(agentSet);
        }

        // POST: AgentSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhoneNumber,Email,AgentName")] AgentSet agentSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agentSet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentSet);
        }

        // GET: AgentSets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentSet agentSet = db.AgentSet.Find(id);
            if (agentSet == null)
            {
                return HttpNotFound();
            }
            return View(agentSet);
        }

        // POST: AgentSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgentSet agentSet = db.AgentSet.Find(id);
            db.AgentSet.Remove(agentSet);
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
