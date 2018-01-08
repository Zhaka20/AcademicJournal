using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using AcademicJournal.ViewModels;
using AcademicJournal.DAL.Context;
using AcademicJournal.DAL.Models;
using AcademicJournal.BLL.Services.Abstract;
using AcademicJournal.Extensions;
using AcademicJournal.App_Start;
using AcademicJournal.BLL.Services.Concrete;

namespace AcademicJournal.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private IStudentService service;
        private ApplicationUserManager userManager;
        private ApplicationDbContext db;

        public StudentsController(IStudentService service, ApplicationUserManager userManager,ApplicationDbContext db)
        {
            this.db = db;
            this.service = service;
            this.userManager = userManager;
        }
        // GET: Students
        public async Task<ActionResult> Index()
        {
            IEnumerable<Student> students = await service.GetAllStudentsAsync();
            var studentsVMList = students.ToShowStudentVMList();
            return View(studentsVMList);
        }

        public async Task<ActionResult> Home()
        {
            var studentId = User.Identity.GetUserId();
            var student = await db.Students.Where(s => s.Id == studentId).Include(m => m.Mentor).Include(m => m.Assignments).FirstOrDefaultAsync();
            StudentsHomeVM vm = new StudentsHomeVM
            {
                Student = student,
                Assignment = new Assignment()
            };
            
            return View(vm);
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await service.GetStudentByIDAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            StudentDetailsVM studentVM = student.ToStudentDetailsVM();
            return View(studentVM);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin, Mentor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [Authorize(Roles = "Admin, Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateStudentVM student)
        {
            userManager = StaticConfig.ConfigureApplicationUserManager(userManager);

            if (ModelState.IsValid)
            {
                Student newStudent = student.ToStudentModel();

                var result = await userManager.CreateAsync(newStudent, student.Password);
                if (result.Succeeded)
                {
                    var roleResult = userManager.AddToRole(newStudent.Id, "Student");
                    return RedirectToAction("Index");
                }
                AddErrors(result);         
            }
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin, Mentor")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await service.GetStudentByIDAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            EditStudentVM studentVM = student.ToEditStudentVM();
            return View(studentVM);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditStudentVM student)
        {    
            if (ModelState.IsValid)
            {
                Student newStudent = student.ToStudentModel();
                service.UpdateStudent(newStudent);
                await service.SaveChangesAsync();
                return RedirectToAction("Student", "Mentors", new { id = newStudent.Id });
            }
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin, Mentor")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await service.GetStudentByIDAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            DeleteStudentVM delStudent = student.ToDeleteStudentVM();
            return View(delStudent);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Mentor")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await service.DeleteStudentByIdAsync(id);
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
