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
    public class TransactionSetsController : Controller
    {
        private Entities db = new Entities();

        // GET: TransactionSets
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            //If the current user is admin, return all the transactions
            if (userId == "f7c59124-09b8-4d89-ba35-8a805911d591")
            {
                return View(db.TransactionSet.Include(t => t.AspNetUsers).Include(t => t.HouseSet).ToList());
            }
            //Only select transactions belong to current user
            var transactionSet = db.TransactionSet.Where(s => s.AspNetUsersId ==userId).Include(t => t.AspNetUsers).Include(t => t.HouseSet);
            return View(transactionSet.ToList());
        }

        // GET: TransactionSets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionSet transactionSet = db.TransactionSet.Find(id);
            if (transactionSet == null)
            {
                return HttpNotFound();
            }
            return View(transactionSet);
        }

        // GET: TransactionSets/Create
        public ActionResult Create()
        {
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.House_Id = new SelectList(db.HouseSet, "Id", "Address");
            ViewBag.Agent_Id = new SelectList(db.AgentSet, "Id", "AgentName");
            return View();
        }

        // POST: TransactionSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Time,Price,Type,AspNetUsersId,House_Id,Agent_Id")] TransactionSet transactionSet)
        {   
            //Get current user Id
            transactionSet.AspNetUsersId = User.Identity.GetUserId();

            //Get current datetime 
            transactionSet.Time = DateTime.Now;

            ModelState.Clear();
            TryValidateModel(transactionSet);
            
            if (ModelState.IsValid)
            {
                db.TransactionSet.Add(transactionSet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", transactionSet.AspNetUsersId);
            ViewBag.House_Id = new SelectList(db.HouseSet, "Id", "Address", transactionSet.House_Id);
            ViewBag.Agent_Id = new SelectList(db.AgentSet, "Id", "AgentName", transactionSet.Agent_Id);
            return View(transactionSet);
        }

        // GET: TransactionSets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionSet transactionSet = db.TransactionSet.Find(id);
            if (transactionSet == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", transactionSet.AspNetUsersId);
            ViewBag.House_Id = new SelectList(db.HouseSet, "Id", "Address", transactionSet.House_Id);
            ViewBag.Agent_Id = new SelectList(db.AgentSet, "Id", "AgentName", transactionSet.Agent_Id);
            return View(transactionSet);
        }

        // POST: TransactionSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Time,Price,Type,AspNetUsersId,House_Id,Agent_Id")] TransactionSet transactionSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionSet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", transactionSet.AspNetUsersId);
            ViewBag.House_Id = new SelectList(db.HouseSet, "Id", "Address", transactionSet.House_Id);
            ViewBag.Agent_Id = new SelectList(db.AgentSet, "Id", "AgentName", transactionSet.Agent_Id);
            return View(transactionSet);
        }

        // GET: TransactionSets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionSet transactionSet = db.TransactionSet.Find(id);
            if (transactionSet == null)
            {
                return HttpNotFound();
            }
            return View(transactionSet);
        }

        // POST: TransactionSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionSet transactionSet = db.TransactionSet.Find(id);
            db.TransactionSet.Remove(transactionSet);
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
