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
using AcademicJournal.BLL.Repository.Concrete;
using Microsoft.AspNet.Identity;
using AcademicJournal.App_Start;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AcademicJournal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MentorsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        IMentorService service;
        public MentorsController()
        {
            service = new MentorService(new MentorRepository(db));
        }
        public MentorsController(IMentorService service)
        {
            this.service = service;
        }

        // GET: Mentors
        public async Task<ActionResult> Index()
        {
            return View(await service.GetAllMentorsAsync());
        }

        // GET: Mentors/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mentor mentor = await service.GetMentorByIDAsync(id);
            if (mentor == null)
            {
                return HttpNotFound();
            }
            MentorDetailsVM mentorVM = mentor.ToMentorDetailsVM();
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
            //if (ModelState.IsValid)
            //{
            //    Mentor newMentor = mentor.ToMentorModel();
            //    service.CreateMentor(newMentor);
            //    await service.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}
            //return View(mentor);
            var userManager = new UserManager<Mentor>(new UserStore<Mentor>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            userManager.PasswordValidator = StaticConfig.GetPasswordValidator();

            var userPassword = StaticConfig.DEFAULT_PASSWORD;
           
            if (ModelState.IsValid)
            {
                Mentor newMenotor = mentor.ToMentorModel();
                var result = await userManager.CreateAsync(newMenotor, userPassword);
                if (result.Succeeded)
                {
                    var roleResult = userManager.AddToRole(newMenotor.Id, "Mentor");
                }
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
            Mentor mentor = await service.GetMentorByIDAsync(id);
            if (mentor == null)
            {
                return HttpNotFound();
            }

            EditMentorVM mentorVM = mentor.ToEditMentorVM();
            return View(mentorVM);
        }

        // POST: Mentors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mentor mentor = await service.GetMentorByIDAsync(id);
            if (mentor == null)
            {
                return HttpNotFound();
            }
            DeleteMentorVM delMentor = mentor.ToDeleteMentorVM();
            return View(delMentor);
        }

        // POST: Mentors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await service.DeleteMentorAsync(id);
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
    }
}
