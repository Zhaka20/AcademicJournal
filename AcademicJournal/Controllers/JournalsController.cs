using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AcademicJournal.DAL.Context;
using AcademicJournal.DAL.Models;
using AcademicJournal.ViewModels;
using Microsoft.AspNet.Identity;

namespace AcademicJournal.Controllers
{

    public class JournalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Fill(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var journal = await db.Journals.Include(j => j.WorkDays).FirstOrDefaultAsync(j => j.Id == id);
            JournalFillVM vm = new JournalFillVM
            {
                Journal = journal,
                WorkDayModel = new WorkDay()
            };
            return View(vm);
        }

        //public async Task<ActionResult> AddAttendees(int? id)
        //{
            
        //}

        public async Task<ActionResult> CreateWorkDay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateWorkDay([Bind(Include = "Id,Day")] WorkDay workDay, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var journal = await db.Journals.FindAsync(id);
                journal.WorkDays.Add(workDay);
                await db.SaveChangesAsync();
                return RedirectToAction("Fill", new { id = id });
            }
            return View(workDay);
        }



        // GET: Journals
        public async Task<ActionResult> Index()
        {
            var journals = db.Journals.Include(j => j.Mentor);
            return View(await journals.ToListAsync());
        }

        // GET: Journals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journal journal = await db.Journals.FindAsync(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            return View(journal);
        }

        // GET: Journals/Create
        public ActionResult Create()
        {
            ViewBag.MentorId = new SelectList(db.Mentors, "Id", "FirstName");
            return View();
        }

        // POST: Journals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Year,MentorId")] Journal journal)
        {
            if (ModelState.IsValid)
            {
                db.Journals.Add(journal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MentorId = new SelectList(db.Mentors, "Id", "FirstName", journal.MentorId);
            return View(journal);
        }

        // GET: Journals/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journal journal = await db.Journals.FindAsync(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            ViewBag.MentorId = new SelectList(db.Mentors, "Id", "FirstName", journal.MentorId);
            return View(journal);
        }

        // POST: Journals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Year,MentorId")] Journal journal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journal).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MentorId = new SelectList(db.Mentors, "Id", "FirstName", journal.MentorId);
            return View(journal);
        }

        // GET: Journals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journal journal = await db.Journals.FindAsync(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            return View(journal);
        }

        // POST: Journals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Journal journal = await db.Journals.FindAsync(id);
            db.Journals.Remove(journal);
            await db.SaveChangesAsync();
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
