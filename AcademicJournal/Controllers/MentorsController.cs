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
using AcademicJournal.BLL.Services.Abstract;
using AcademicJournal.Extensions;
using AcademicJournal.BLL.Services.Concrete;
using Microsoft.AspNet.Identity;
using AcademicJournal.App_Start;
using Microsoft.AspNet.Identity.EntityFramework;
using AcademicJournal.Services.Abstractions;

namespace AcademicJournal.Controllers
{
    [Authorize]
    public class MentorsController : Controller
    {

        IMentorsControllerService _service;
        IMentorService service;
        ApplicationUserManager userManager;

        // remove after refactoring 
        ApplicationDbContext db;
        public MentorsController(IMentorsControllerService service)
        {
            this._service = service;
        }
        public MentorsController(IMentorService service, ApplicationUserManager userManager, ApplicationDbContext db)
        {
            this.service = service;
            this.userManager = userManager;
            this.db = db;
        }

        [Authorize(Roles = "Mentor")]
        public async Task<ActionResult> Home()
        {
            var mentorId = User.Identity.GetUserId();
            var mentor = await db.Mentors.Where(m => m.Id == mentorId).Include(m => m.Students).Include(m => m.Assignments).FirstOrDefaultAsync();
            MentorsHomeVM vm = new MentorsHomeVM
            {
                Mentor = mentor,
                JournalVM = new Journal()
            };
            return View(vm);
        }

        // GET: Mentors
        [ActionName("Index")]
        public async Task<ActionResult> ListAllMentors()
        {
            MentorsListVM vm = new MentorsListVM
            {
                Mentors = await service.GetAllMentorsAsync()
            };
            return View(vm);
        }

        // GET: Mentors/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mentor mentor = await service.GetMentorByIdAsync(id);
            if (mentor == null)
            {
                return HttpNotFound();
            }
            MentorDetailsVM mentorVM = mentor.ToMentorDetailsVM();
            return View(mentorVM);
        }

        public async Task<ActionResult> AcceptStudent()
        {
            var mentorId = User.Identity.GetUserId();
            var students = await db.Students.Where(s => s.Mentor.Id != mentorId).ToListAsync();
            return View(students);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AcceptStudent(string id)
        {
            await service.AcceptStudentAsync(id, User.Identity.GetUserId());
            await service.SaveChangesAsync();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public async Task<ActionResult> ExpelStudent(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [ActionName("ExpelStudent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveStudent(string id)
        {
            await service.RemoveStudentAsync(id, User.Identity.GetUserId());
            await service.SaveChangesAsync();
            return RedirectToAction("Home", "Mentors", new { id = id });
        }

        public async Task<ActionResult> Student(string id)
        {
            var student = await db.Students.Where(s => s.Id == id).
                                            Include(m => m.Mentor).
                                            Include(s => s.Submissions.Select(sub => sub.SubmitFile)).
                                            Include(s => s.Submissions.Select(sub => sub.Assignment.AssignmentFile)).
                                            FirstOrDefaultAsync();
            StudentMentorVM vm = new StudentMentorVM
            {
                Student = student,
                AssignmentModel = new Assignment(),
                SubmissionModel = new Submission()
            };

            return View(vm);
        }

        // GET: Mentors/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mentors/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateMentorVM mentor)
        {
            userManager = StaticConfig.ConfigureApplicationUserManager(userManager);

            if (ModelState.IsValid)
            {
                Mentor newMenotor = mentor.ToMentorModel();
                var result = await userManager.CreateAsync(newMenotor, mentor.Password);
                if (result.Succeeded)
                {
                    var roleResult = userManager.AddToRole(newMenotor.Id, "Mentor");
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(mentor);
        }

        // GET: Mentors/Edit/5
        [Authorize(Roles = "Admin, Mentor")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mentor mentor = await service.GetMentorByIdAsync(id);
            if (mentor == null)
            {
                return HttpNotFound();
            }

            EditMentorVM mentorVM = mentor.ToEditMentorVM();
            return View(mentorVM);
        }

        // POST: Mentors/Edit/5    
        [Authorize(Roles = "Admin, Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditMentorVM vm)
        {
            if (ModelState.IsValid)
            {
                Mentor newMentor = vm.ToMentorModel();
                service.UpdateMentor(newMentor);
                await service.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Mentors/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mentor mentor = await service.GetMentorByIdAsync(id);
            if (mentor == null)
            {
                return HttpNotFound();
            }
            DeleteMentorVM delMentor = mentor.ToDeleteMentorVM();
            return View(delMentor);
        }

        // POST: Mentors/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await service.DeleteMentorByIdAsync(id);
            await service.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                service.Dispose();
            }
            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
