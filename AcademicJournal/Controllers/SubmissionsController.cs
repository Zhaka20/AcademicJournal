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
using Microsoft.AspNet.Identity;

namespace AcademicJournal.Controllers
{
    public class SubmissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Submissions
        public async Task<ActionResult> Index()
        {
            var submissions = db.Submissions.Include(s => s.Assignment).Include(s => s.Student).Include(s => s.SubmitFile);
            return View(await submissions.ToListAsync());
        }


        public async Task<ActionResult> Assignment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.Include(a => a.AssignmentFile).
                                                         Include(a => a.Creator).
                                                         Include(a => a.Submissions.Select(s => s.Student)).
                                                         Include(a => a.Submissions.Select(s => s.SubmitFile)).
                                                         FirstOrDefaultAsync(a => a.AssignmentId == id);
            if (assignment == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            AssignmentSumbissionsVM vm = new AssignmentSumbissionsVM
            {
                Assignment = assignment,
                Submissions = assignment.Submissions,
                SubmissionModel = new Submission(),
                StudentModel = new Student()
            };
            return View(vm);
        }
     
        // GET: Submissions/Details/5
        [Route("submissions/details/{assignmentId:int}/{studentId}")]
        public async Task<ActionResult> Details(int assignmentId, string studentId)
        {
            if (studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submission submission = await db.Submissions.FindAsync(assignmentId,studentId);
            if (submission == null)
            {
                return HttpNotFound();
            }
            return View(submission);
        }

        // GET: Submissions/Create
        public ActionResult Create()
        {
            ViewBag.AssignmentId = new SelectList(db.Assignments, "AssignmentId", "Title");
            ViewBag.StudentId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.Id = new SelectList(db.SubmitFiles, "Id", "FileGuid");
            return View();
        }

        // POST: Submissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Grade,Completed,DueDate,Submitted,StudentId,AssignmentId")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                db.Submissions.Add(submission);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.AssignmentId = new SelectList(db.Assignments, "AssignmentId", "Title", submission.AssignmentId);
            //ViewBag.StudentId = new SelectList(db.Users, "Id", "FirstName", submission.StudentId);
            //ViewBag.Id = new SelectList(db.SubmitFiles, "Id", "FileGuid", submission.Id);
            return View(submission);
        }

        // GET: Submissions/Edit/5
        [HttpGet]
        [Route("submissions/edit/{assignmentId:int}/{studentId}")]
        public async Task<ActionResult> Edit(int assignmentId, string studentId)
        {
            if (studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Submission submission = await db.Submissions.FindAsync(assignmentId,studentId);
            if (submission == null)
            {
                return HttpNotFound();
            }

            var vm = new EditSubmissionVM
            {
                StudentId = studentId,
                AssignmentId = assignmentId,
                Completed = submission.Completed,
                DueDate = submission.DueDate,
                Grade = (int)submission.Grade
            };
            return View(vm);
        }

        // POST: Submissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("submissions/edit/{assignmentId:int}/{studentId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSubmissionVM submission)
        {
            if (!ModelState.IsValid)
            {
                return View(submission);
            }

            Submission newSubmission = new Submission
            {
                AssignmentId = submission.AssignmentId,
                StudentId = submission.StudentId,
                Grade = (byte?)submission.Grade,
                DueDate = submission.DueDate,
                Completed = submission.Completed
            };

            db.Entry(newSubmission).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "Submissions", new { assignmentId = newSubmission.AssignmentId, studentId = newSubmission.StudentId });
        }

        // GET: Submissions/Delete/5
        public async Task<ActionResult> Delete(int assignmentId, string studentId)
        {
            if (studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Submission submission = await db.Submissions.FindAsync(assignmentId,studentId);
            if (submission == null)
            {
                return HttpNotFound();
            }
            return View(submission);
        }

        // POST: Submissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int assignmentId, string studentId)
        {
            Submission submission = await db.Submissions.FindAsync(assignmentId, studentId);
            if(submission.SubmitFile != null)
            {
                DeleteFile(submission.SubmitFile);
            }
            db.Submissions.Remove(submission);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Route("assignments/DownloadSubmissionFile/{assignmentId:int}/{studentId}")]
        public async Task<FileResult> DownloadSubmissionFile(int assignmentId, string studentId)
        {
            if (studentId == null)
            {
                throw new ArgumentNullException();
            }
            var submission = await db.Submissions.FindAsync(assignmentId,studentId);

            string origFileName = submission.SubmitFile.FileName;
            string fileGuid = submission.SubmitFile.FileGuid;
            string filePath = Path.Combine(Server.MapPath("~/Files/Assignments"), fileGuid);
            string mimeType = MimeMapping.GetMimeMapping(origFileName);
            return File(filePath, mimeType, origFileName);
        }

        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [Route("Submissions/toggleStatus/{assignmentId:int}/{studentId}")]
        public async Task<ActionResult> ToggleStatus(int assignmentId, string studentId)
        {
            var submission = await db.Submissions.FindAsync(assignmentId,studentId);
            if (submission == null)
            {
                return HttpNotFound();
            }
            submission.Completed = submission.Completed == true ? false : true;
            await db.SaveChangesAsync();
            return RedirectToAction("Student", "Mentors", new { id = submission.StudentId });
        }


        [Authorize(Roles = "Mentor")]
        [Route("Submissions/evaluate/{assignmentId:int}/{studentId}")]
        public async Task<ActionResult> Evaluate(int assignmentId, string studentId)
        {
            var submission = await db.Submissions.FindAsync(assignmentId, studentId);
            var vm = new EvaluateSubmissionVM
            {
                Submission = submission,
                Grade = (byte)(submission.Grade ?? 0)
            };
            return View(vm);
        }

        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [Route("Submissions/evaluate/{assignmentId:int}/{studentId}")]
        public async Task<ActionResult> Evaluate(EvaluateSubmissionInputModel model, int assignmentId, string studentId)
        {
            Submission submission = await db.Submissions.FindAsync(assignmentId, studentId);
            if(submission == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (ModelState.IsValid)
            {
                submission.Grade = (byte)model.Grade;
                await db.SaveChangesAsync();
                return RedirectToAction("Student", "Mentors", new { id = submission.StudentId });
            }

            var vm = new EvaluateSubmissionVM
            {
                Submission = submission,
                Grade = (byte)(submission.Grade ?? 0)
            };
            return View(vm);
        }

        private void DeleteFile(DAL.Models.FileInfo file)
        {
            if (file == null) return;

            string fullPath = Path.Combine(Server.MapPath("~/Files/Assignments"), file.FileGuid);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        [Authorize(Roles = "Student")]
        public ActionResult UploadFile(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> UploadFile(HttpPostedFileBase file, int id)
        {
            var studentId = User.Identity.GetUserId();
            var submission = await db.Submissions.FindAsync(id, studentId);
            if(submission == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    if (submission.Completed)
                    {
                        return Content("The assignment is already complete! You cannot updload a file to this assignment.");
                    }

                    SubmitFile newSubmitFile = new SubmitFile
                    {
                        FileName = file.FileName,
                        FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                    };

                    string path = Path.Combine(Server.MapPath("~/Files/Assignments"), newSubmitFile.FileGuid);
                    file.SaveAs(path);

                    if (submission.SubmitFile != null)
                    {
                        DeleteFile(submission.SubmitFile);
                        db.SubmitFiles.Remove(submission.SubmitFile);
                    }

                    submission.SubmitFile = newSubmitFile;
                    submission.Submitted = DateTime.Now;

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
