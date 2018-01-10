using AcademicJournal.DAL.Context;
using AcademicJournal.DAL.Models;
using AcademicJournal.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AcademicJournal.Controllers
{
    public class SubmissionsController : Controller
    {
        private ApplicationDbContext db;

        public SubmissionsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<ActionResult> Index()
        {
            return View(await db.Submissions.ToListAsync());
        }

        public async Task<FileResult> DownloadSubmitFile(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var submission = await db.Submissions.FindAsync(id);

            string origFileName = submission.SubmitFile.FileName;
            string fileGuid = submission.SubmitFile.FileGuid;
            string filePath = Path.Combine(Server.MapPath("~/Files/Assignments"), fileGuid);
            string mimeType = MimeMapping.GetMimeMapping(origFileName);
            return File(filePath, mimeType, origFileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> Submit(HttpPostedFileBase file, int id)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    Submission submission = await db.Submissions.Where(a => a.Id == id).
                                                                 FirstOrDefaultAsync();

                    if (submission.Completed)
                    {
                        return Content("The assignment is already complete! You cannot updload a file to this assignment.");
                    }
                    TaskFile submitFile = new TaskFile
                    {
                        FileName = file.FileName,
                        FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                    };

                    string path = Path.Combine(Server.MapPath("~/Files/Assignments"), submitFile.FileGuid);
                    file.SaveAs(path);


                    if (submission.SubmitFile != null)
                    {
                        DeleteFile(submission.SubmitFile);
                    }

                    submission.SubmitFile = submitFile;
                    db.Entry(submitFile).State = EntityState.Added;
                    await db.SaveChangesAsync();
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception ex)
                {
                    ViewBag.FileStatus = "Error while file uploading.";
                }
            }
            else
            {
                ModelState.AddModelError("", "Upload file is not selected!");
            }
            return View();
        }

        [Authorize(Roles = "Mentor")]
        public async Task<ActionResult> Evaluate(int? id)
        {
            var submission = await db.Submissions.FindAsync(id);
            EvaluateAssignmentVM vm = new EvaluateAssignmentVM
            {
                Assignment = submission,
                Grade = (byte)(submission.Grade ?? 0)
            };
            return View(vm);
        }

        [ActionName("Evaluate")]
        [Authorize(Roles = "Mentor")]
        [HttpPost]
        public async Task<ActionResult> EvaluatePost(int grade, int? id)
        {
            Assignment assignment = await db.Assignments.FindAsync(id);
            if (ModelState.IsValid)
            {
                assignment.Grade = (byte)grade;
                await db.SaveChangesAsync();
                return RedirectToAction("Student", "Mentors", new { id = assignment.StudentId });
            }

            EvaluateAssignmentVM vm = new EvaluateAssignmentVM
            {
                Assignment = assignment,
                Grade = (byte)(assignment.Grade ?? 0)
            };
            return View(vm);
        }

        private void DeleteFile(TaskFile file)
        {
            if (file == null) return;

            string fullPath = Path.Combine(Server.MapPath("~/Files/Assignments"), file.FileGuid);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
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