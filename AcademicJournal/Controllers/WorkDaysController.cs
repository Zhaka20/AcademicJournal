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
using Microsoft.AspNet.Identity;
using AcademicJournal.ViewModels;

namespace AcademicJournal.Controllers
{
    public class WorkDaysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkDays
        public async Task<ActionResult> Index()
        {
            return View(await db.WorkDays.ToListAsync());
        }

        // GET: WorkDays/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDay workDay = await db.WorkDays.Include(w => w.Attendances).FirstOrDefaultAsync(w => w.Id == id);
            if (workDay == null)
            {
                return HttpNotFound();
            }

            var vm = new WorkDaysDetailsVM
            {
                WorkDay = workDay,
                AttendanceModel = new Attendance()
            };
            return View(vm);
        }

        // GET: WorkDays/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDay vm = new WorkDay
            {
                JournalId = (int)id,
                Day = DateTime.Now
            };
            return View(vm);
        }

        // POST: WorkDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "JournalId,Day")] WorkDay workDay)
        {
            if (ModelState.IsValid)
            {
                db.WorkDays.Add(workDay);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(workDay);
        }

        // GET: WorkDays/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDay workDay = await db.WorkDays.FindAsync(id);
            if (workDay == null)
            {
                return HttpNotFound();
            }
            return View(workDay);
        }

        // POST: WorkDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Day")] WorkDay workDay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workDay).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workDay);
        }

        // GET: WorkDays/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDay workDay = await db.WorkDays.FindAsync(id);
            if (workDay == null)
            {
                return HttpNotFound();
            }
            return View(workDay);
        }

        // POST: WorkDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkDay workDay = await db.WorkDays.FindAsync(id);
            db.WorkDays.Remove(workDay);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddAttendees(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mentorId = User.Identity.GetUserId();
            var mentorsAllStudents = db.Students.Where(s => s.MentorId == mentorId);

            var presentStudents = from attendance in db.Attendances
                                  where attendance.WorkDayId == id
                                  select attendance.Student;

            //var notPresentStudents = from student in mentorsAllStudents
            //                         from presentStudent in presentStudents
            //                         where (student.Id != presentStudent.Id)
            //                         select student;

            var notPresentStudents = mentorsAllStudents.Except(presentStudents);

            return View(await notPresentStudents.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAttendees(int? id, List<string> studentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (studentId != null)
            {
                WorkDay workDay = await db.WorkDays.FindAsync(id);

                var query = from student in db.Students
                            where studentId.Contains(student.Id)
                            select student;

                var listOfStudents = await query.ToListAsync();

                foreach (var student in listOfStudents)
                {
                    workDay.Attendances.Add(new Attendance { Student = student, Come = DateTime.Now });
                }
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Details", "WorkDays", new { id = id });
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
