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
using System.IO;
using AcademicJournal.BLL.Services.Concrete;
using AcademicJournal.BLL.Repository.Concrete;
using AcademicJournal.BLL.Services.Abstract;

namespace AcademicJournal.Controllers
{
    [Authorize(Roles = "Mentor")]
    public class AssignmentsController : Controller
    {
        private ApplicationDbContext db;
        private IMentorService mentorService;

        public AssignmentsController(ApplicationDbContext db, IMentorService service)
        {
            this.db = db;
            this.mentorService = service;
        }

        // GET: Assignments
        public async Task<ActionResult> Index()
        {
            return View(await db.Assignments.ToListAsync());
        }

        // GET: Assignments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.FindAsync(id);
                //AsQueryable().Include(a => a.TaskFile).FirstOrDefaultAsync(a => a.AssignmentId == id);

            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignments/Create
        public ActionResult Create(string id)
        {
            return View();
        }

        // POST: Assignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAssigmentVM assignment, HttpPostedFileBase file, string id)
        {
            if (ModelState.IsValid)
            {
                if(file != null && file.ContentLength > 0)
                {
                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/Files/Assignments"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);

                        Mentor mentor = await mentorService.GetMentorByEmailAsync(User.Identity.Name);

                        TaskFile taskFile = new TaskFile
                        {
                            FileName = file.FileName,
                            UploadFile = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                        };
                        
                        Assignment assignmentModel = new Assignment
                        {
                            Title = assignment.Title,
                            Created = DateTime.Now,
                            CreatorId = mentor.Id,
                            DueDate = assignment.DueDate,
                            TaskFile = taskFile,
                            StudentId = id
                        };

                        db.Assignments.Add(assignmentModel);
                        await db.SaveChangesAsync();
                        db.Entry(taskFile).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.FileStatus = "File uploaded successfully.";
                    }               
                    catch (Exception ex)
                    {
                        ViewBag.FileStatus = "Error while file uploading."; 
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Upload file is not selected!");
                }
            }
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AssignmentId,Title,Description,UploadFile,Completed,Grade,Created,Submitted,DueDate")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Assignment assignment = await db.Assignments.FindAsync(id);
            db.Assignments.Remove(assignment);
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
