using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AcademicJournal.DAL.Context;
using Microsoft.AspNet.Identity;
using AcademicJournal.DataModel.Models;

namespace AcademicJournal.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db;

        public CommentsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        // GET: Comments
        public async Task<ActionResult> Index()
        {
            var comments = db.Comments.Include(c => c.Assignment).Include(c => c.Author);
            return View(await comments.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.AssignmentId = new SelectList(db.Assignments, "AssignmentId", "Title");
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Mentor, Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Comment comment, int id)
        {
            //check if user is owner or mentor is users current mentor!
            if (ModelState.IsValid)
            {
                comment.Created = DateTime.Now;
                comment.AssignmentId = id;
                comment.AuthorId = User.Identity.GetUserId();
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Assignments" , new { id = id });
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Text,AssignmentId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Edited = DateTime.Now;
                db.Comments.Attach(comment);
                db.Entry(comment).Property(c => c.Edited).IsModified = true;
                db.Entry(comment).Property(c => c.Text).IsModified = true;

                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Assignments", new { id = comment.AssignmentId });
            }
            ViewBag.AssignmentId = new SelectList(db.Assignments, "AssignmentId", "Title", comment.AssignmentId);
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", comment.AuthorId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            db.Comments.Remove(comment);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "Assignments", new { id = comment.AssignmentId });
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
