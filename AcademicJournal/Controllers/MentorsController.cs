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

namespace AcademicJournal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MentorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Mentors
        public async Task<ActionResult> Index()
        {
            return View(await db.Mentors.ToListAsync());
        }

        // GET: Mentors/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mentor mentor = await db.Mentors.FindAsync(id);
            if (mentor == null)
            {
                return HttpNotFound();
            }
            MentorDetailsVM mentorVM = new MentorDetailsVM()
            {
                Email = mentor.Email,
                FirstName = mentor.FirstName,
                LastName = mentor.LastName,
                PhoneNumber = mentor.PhoneNumber
            };
            return View(mentorVM);
        }

        // GET: Mentors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mentors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateMentorVM mentor)
        {
            if (ModelState.IsValid)
            {
                Mentor newMentor = new Mentor()
                {
                    Email = mentor.Email,
                    UserName = mentor.UserName,
                    FirstName = mentor.FirstName,
                    LastName = mentor.LastName,
                    PhoneNumber = mentor.PhoneNumber
                };

                db.Mentors.Add(newMentor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mentor);
        }

        // GET: Mentors/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mentor mentor = await db.Mentors.FindAsync(id);
            if (mentor == null)
            {
                return HttpNotFound();
            }

            EditMentorVM mentorVM = new EditMentorVM
            {
                Email = mentor.Email,
                FirstName = mentor.FirstName,
                LastName = mentor.LastName,
                PhoneNumber = mentor.PhoneNumber
            };

            return View(mentorVM);
        }

        // POST: Mentors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditMentorVM mentor)
        {
            if (ModelState.IsValid)
            {
                Mentor newMentor = new Mentor
                {
                    UserName = mentor.UserName,
                    Email = mentor.Email,
                    FirstName = mentor.FirstName,
                    LastName = mentor.LastName,
                    PhoneNumber = mentor.PhoneNumber,
                    Id = mentor.Id
                };

                db.Entry(newMentor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mentor);
        }

        // GET: Mentors/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mentor mentor = await db.Mentors.FindAsync(id);
            if (mentor == null)
            {
                return HttpNotFound();
            }
            DeleteMentorVM delMentor = new DeleteMentorVM
            {
                Email = mentor.Email,
                FirstName = mentor.FirstName,
                LastName = mentor.LastName,
                PhoneNumber = mentor.PhoneNumber
            };

            return View(delMentor);
        }

        // POST: Mentors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Mentor mentor = await db.Mentors.FindAsync(id);
            db.Mentors.Remove(mentor);
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
