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
    public class RateSetsController : Controller
    {
        private Entities db = new Entities();

        // GET: RateSets
        public ActionResult Index()
        {
            return View(db.RateSet.ToList());
        }

        // GET: RateSets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateSet rateSet = db.RateSet.Find(id);
            if (rateSet == null)
            {
                return HttpNotFound();
            }
            return View(rateSet);
        }

        // GET: RateSets/Create
        public ActionResult Create()
        {
            ViewData["MarkList"] = GetMarkList();
            return View();
        }

        // POST: RateSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AgentMark,HouseMark,WebSiteMark,OverallMark")] RateSet rateSet)
        {
            if (ModelState.IsValid)
            {
                db.RateSet.Add(rateSet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rateSet);
        }

        // GET: RateSets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateSet rateSet = db.RateSet.Find(id);
            if (rateSet == null)
            {
                return HttpNotFound();
            }
            return View(rateSet);
        }

        // POST: RateSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AgentMark,HouseMark,WebSiteMark,OverallMark")] RateSet rateSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rateSet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rateSet);
        }

        // GET: RateSets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateSet rateSet = db.RateSet.Find(id);
            if (rateSet == null)
            {
                return HttpNotFound();
            }
            return View(rateSet);
        }

        // POST: RateSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RateSet rateSet = db.RateSet.Find(id);
            db.RateSet.Remove(rateSet);
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

        //Create and return a markList from 0 to 10
        private List<SelectListItem> GetMarkList()
        { 
            List<SelectListItem> markList = new List<SelectListItem>();

            for (int i = 0; i <= 10; i++)
            {
                markList.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString()});
            }

            return markList;
        }

        //Get and return overall mark to display
        public ActionResult GetData()
        {
            int agentMark0 = db.RateSet.Where(x => x.OverallMark == 0).Count();
            int agentMark1 = db.RateSet.Where(x => x.OverallMark == 1).Count();
            int agentMark2 = db.RateSet.Where(x => x.OverallMark == 2).Count();
            int agentMark3 = db.RateSet.Where(x => x.OverallMark == 3).Count();
            int agentMark4 = db.RateSet.Where(x => x.OverallMark == 4).Count();
            int agentMark5 = db.RateSet.Where(x => x.OverallMark == 5).Count();
            int agentMark6 = db.RateSet.Where(x => x.OverallMark == 6).Count();
            int agentMark7 = db.RateSet.Where(x => x.OverallMark == 7).Count();
            int agentMark8 = db.RateSet.Where(x => x.OverallMark == 8).Count();
            int agentMark9 = db.RateSet.Where(x => x.OverallMark == 9).Count();
            int agentMark10 = db.RateSet.Where(x => x.OverallMark == 10).Count();

            Ratio obj = new Ratio();
            obj.agentMark0 = agentMark0;
            obj.agentMark1 = agentMark1;
            obj.agentMark2 = agentMark2;
            obj.agentMark3 = agentMark3;
            obj.agentMark4 = agentMark4;
            obj.agentMark5 = agentMark5;
            obj.agentMark6 = agentMark6;
            obj.agentMark7 = agentMark7;
            obj.agentMark8 = agentMark8;
            obj.agentMark9 = agentMark9;
            obj.agentMark10 = agentMark10;

            //Return as Json
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public class Ratio
        {
            public int agentMark0 { get; set; }
            public int agentMark1 { get; set; }
            public int agentMark2 { get; set; }
            public int agentMark3 { get; set; }
            public int agentMark4 { get; set; }
            public int agentMark5 { get; set; }
            public int agentMark6 { get; set; }
            public int agentMark7 { get; set; }
            public int agentMark8 { get; set; }
            public int agentMark9 { get; set; }
            public int agentMark10 { get; set; }
        }
    }
}
