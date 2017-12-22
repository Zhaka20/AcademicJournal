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
using AcademicJournal.BLL.Repository;
using AcademicJournal.BLL.Services.Abstract;
using AcademicJournal.Extensions;
using AcademicJournal.App_Start;
using AcademicJournal.BLL.Services.Concrete;
using AcademicJournal.BLL.Repository.Abstract;
using AcademicJournal.BLL.Repository.Concrete;

namespace AcademicJournal.Controllers
{
    [Authorize(Roles = "Admin, Mentor")]
    public class StudentsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private IStudentService service;
        public StudentsController()
        {
            this.service = new StudentService(new StudentRepository(db));
        }
        public StudentsController(IStudentService service)
        {
            this.service = service;
        }
        // GET: Students
        public async Task<ActionResult> Index()
        {
            IEnumerable<Student> students = await service.GetAllStudentsAsync();
            var studentsVMList = students.ToShowStudentVMList();
            return View(studentsVMList);
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateStudentVM student)
        {
            var userManager = new UserManager<Student>(new UserStore<Student>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            userManager.PasswordValidator = StaticConfig.GetPasswordValidator();

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditStudentVM student)
        {    
            if (ModelState.IsValid)
            {
                Student newStudent = student.ToStudentModel();
                service.UpdateStudent(newStudent);
                await service.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
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
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await service.DeleteStudentAsync(id);
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
