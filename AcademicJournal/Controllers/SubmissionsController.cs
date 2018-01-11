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

namespace AcademicJournal.Controllers
{
    public class SubmissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Submissions
        public async Task<ActionResult> Index()
        {
            var submissions = db.Submissions.Include(s => s.Assignment).Include(s => s.Student).Include(s => s.SubmitFile);
            return View(await submissions.ToListAsync());
        }

        // GET: Submissions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submission submission = await db.Submissions.FindAsync(id);
            if (submission == null)
            {
                return HttpNotFound();
            }
            return View(submission);
        }

        // GET: Submissions/Create
        public ActionResult Create()
        {
            ViewBag.AssignmentId = new SelectList(db.Assignments, "AssignmentId", "Title");
            ViewBag.StudentId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.Id = new SelectList(db.SubmitFiles, "Id", "FileGuid");
            return View();
        }

        // POST: Submissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Grade,Completed,DueDate,Submitted,StudentId,AssignmentId")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                db.Submissions.Add(submission);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AssignmentId = new SelectList(db.Assignments, "AssignmentId", "Title", submission.AssignmentId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "FirstName", submission.StudentId);
            ViewBag.Id = new SelectList(db.SubmitFiles, "Id", "FileGuid", submission.Id);
            return View(submission);
        }

        // GET: Submissions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submission submission = await db.Submissions.FindAsync(id);
            if (submission == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignmentId = new SelectList(db.Assignments, "AssignmentId", "Title", submission.AssignmentId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "FirstName", submission.StudentId);
            ViewBag.Id = new SelectList(db.SubmitFiles, "Id", "FileGuid", submission.Id);
            return View(submission);
        }

        // POST: Submissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Grade,Completed,DueDate,Submitted,StudentId,AssignmentId")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(submission).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AssignmentId = new SelectList(db.Assignments, "AssignmentId", "Title", submission.AssignmentId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "FirstName", submission.StudentId);
            ViewBag.Id = new SelectList(db.SubmitFiles, "Id", "FileGuid", submission.Id);
            return View(submission);
        }

        // GET: Submissions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submission submission = await db.Submissions.FindAsync(id);
            if (submission == null)
            {
                return HttpNotFound();
            }
            return View(submission);
        }

        // POST: Submissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Submission submission = await db.Submissions.FindAsync(id);
            db.Submissions.Remove(submission);
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
