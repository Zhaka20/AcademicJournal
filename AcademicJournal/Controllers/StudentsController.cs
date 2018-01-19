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
using AcademicJournal.Services.Abstractions;

namespace AcademicJournal.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private ApplicationUserManager userManager;
        private IStudentsControllerService _service;

        public StudentsController(IStudentsControllerService service, ApplicationUserManager userManager)
        {
            this._service = service;
            this.userManager = userManager;
        }
        // GET: Students
        public async Task<ActionResult> Index()
        {
            StudentsIndexViewModel viewModel = await _service.GetIndexViewModelAsync();
            return View(viewModel);
        }

        public async Task<ActionResult> Home()
        {
            string studentId = User.Identity.GetUserId();
            StudentsHomeVM viewModel = await _service.GetHomeViewModelAsync(studentId);
            
            return View(viewModel);
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StudentDetailsVM viewModel = await _service.GetDetailsViewModelAsync(id);
            if(viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin, Mentor")]
        public ActionResult Create()
        {
            CreateStudentVM viewModel = _service.GetCreateStudentViewModel();
            return View(viewModel);
        }

        // POST: Students/Create
        [Authorize(Roles = "Admin, Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateStudentVM viewModel)
        {
            userManager = StaticConfig.ConfigureApplicationUserManager(userManager);

            if (ModelState.IsValid)
            {
                IdentityResult result = await _service.CreateStudentAsync(viewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrors(result);         
            }
            return View(viewModel);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin, Mentor")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditStudentVM viewModel = await _service.GetEditStudentViewModelAsync(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditStudentVM viewModel)
        {    
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateStudentAsync(viewModel);
                    return RedirectToAction("Student", "Mentors", new { id = viewModel.Id });
                }
                catch
                {
                    ViewBag.ErrorMassage = "Could not update the Student. Please try again!";
                }
             
            }
            return View(viewModel);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin, Mentor")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeleteStudentVM viewModel = await _service.GetDeleteStudentViewModelAsync(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Mentor")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _service.DeleteStudentAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "Could not delete the Student. Please try again!";
                return RedirectToAction("Delete", new { id = id });
            }          
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
