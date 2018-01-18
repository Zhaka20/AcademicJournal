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
    public class AttendancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attendances
        public async Task<ActionResult> Index()
        {
            var attendances = db.Attendances.Include(a => a.Day).Include(a => a.Student);
            return View(await attendances.ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = await db.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = await db.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Come,Left")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Attach(attendance);
                db.Entry(attendance).Property(e => e.Left).IsModified = true;
                db.Entry(attendance).Property(e => e.Come).IsModified = true;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.WorkDayId = new SelectList(db.WorkDays, "Id", "Id", attendance.WorkDayId);
            ViewBag.StudentId = new SelectList(db.Users, "Id", "FirstName", attendance.StudentId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = await db.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Attendance attendance = await db.Attendances.FindAsync(id);
            var workDayId = attendance.WorkDayId;
            db.Attendances.Remove(attendance);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "WorkDays", new { id = workDayId});
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
