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
using AcademicJournal.BLL.Services.Abstract;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace AcademicJournal.Controllers
{
    [Authorize]
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
            return View(await db.Assignments.Include(a => a.Student).ToListAsync());
        }

        public async Task<FileResult> DownloadTaskFile(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var assignment = await db.Assignments.FindAsync(id);

            string origFileName = assignment.TaskFile.FileName;
            string fileGuid = assignment.TaskFile.UploadFile;
            string filePath = Path.Combine(Server.MapPath("~/Files/Assignments"), fileGuid);
            string mimeType = MimeMapping.GetMimeMapping(origFileName);
            return File(filePath,mimeType, origFileName);

        }
        public async Task<FileResult> DownloadSubmitFile(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var assignment = await db.Assignments.FindAsync(id);

            string origFileName = assignment.SubmitFile.FileName;
            string fileGuid = assignment.SubmitFile.UploadFile;
            string filePath = Path.Combine(Server.MapPath("~/Files/Assignments"), fileGuid);
            string mimeType = MimeMapping.GetMimeMapping(origFileName);
            return File(filePath, mimeType, origFileName);
        }

        public async Task<ActionResult> CreatedBy(string id)
        {
            return View(await db.Assignments.Where(a => a.CreatorId == id).ToListAsync());
        }

        public async Task<ActionResult> Student(string id)
        {
            Student student = await db.Students.Include(s => s.Assignments).FirstOrDefaultAsync(s => s.Id == id);
            MentorsStudentVM vm = new MentorsStudentVM
            {
                Student = student,
                Assignment = new Assignment()
            };
            return View(vm);
        }
        // GET: Assignments/Details/5
        public async Task<ActionResult> Details(int? id)
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

            AssignmentDetailsVM vm = new AssignmentDetailsVM
            {
                Assignment = assignment,
                Comment = new Comment()
            };
            return View(vm);
        }

        [Authorize(Roles = "Mentor")]
        public async Task<ActionResult> Evaluate(int? id)
        {
            var assignment = await db.Assignments.FindAsync(id);
            EvaluateAssignmentVM vm = new EvaluateAssignmentVM
            {
                Assignment = assignment,
                Grade = (byte)(assignment.Grade ?? 0)
            };
            return View(vm);
        }

        [ActionName("Evaluate")]
        [Authorize(Roles = "Mentor")]
        [HttpPost]
        public async Task<ActionResult> EvaluatePost(int grade,int? id)
        {
            Assignment assignment = await db.Assignments.FindAsync(id);
            if(ModelState.IsValid)
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

        // GET: Assignments/Create/5
        [Authorize(Roles = "Mentor")]
        public ActionResult Create(string id)
        {
            return View();
        }

        // POST: Assignments/Create
        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAssigmentVM assignment, HttpPostedFileBase file, string id)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        Mentor mentor = await mentorService.GetMentorByEmailAsync(User.Identity.Name);
                        TaskFile taskFile = new TaskFile
                        {
                            FileName = file.FileName,
                            UploadFile = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                        };

                        string path = Path.Combine(Server.MapPath("~/Files/Assignments"), taskFile.UploadFile);
                        file.SaveAs(path);

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
                        return RedirectToAction("Student", "Mentors", new { id = id });
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

        // GET: Assignments/Create/5
        [Authorize(Roles = "Student")]
        public ActionResult Submit(string id)
        {
            return View();
        }

        [Authorize(Roles = "Mentor")]
        [HttpPost]
        public async Task<ActionResult> ToggleStatus(int id)
        {
            var assignment = await db.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            assignment.Completed = assignment.Completed == true ? false : true;
            await db.SaveChangesAsync();
            return RedirectToAction("Student","Mentors", new { id = assignment.StudentId});
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
                    Assignment assignment = await db.Assignments.Where(a => a.AssignmentId == id).
                                                                 Include(a => a.SubmitFile).
                                                                 FirstOrDefaultAsync();
                    if (assignment.Completed)
                    {
                        return Content("The assignment is already complete! You cannot updload a file to this assignment.");
                    }
                    TaskFile submitFile = new TaskFile
                    {
                        FileName = file.FileName,
                        UploadFile = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                    };

                    string path = Path.Combine(Server.MapPath("~/Files/Assignments"), submitFile.UploadFile);
                    file.SaveAs(path);

                    
                    if (assignment.SubmitFile != null)
                    {
                        DeleteFile(assignment.SubmitFile);
                    }

                    assignment.SubmitFile = submitFile;
                    await db.SaveChangesAsync();
                    db.Entry(submitFile).State = EntityState.Modified;
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
                ModelState.AddModelError("", "Upload file is not selected!");
            }
            return View();
        }

        // GET: Assignments/Edit/5
        [Authorize(Roles = "Mentor")]
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
            if (assignment.Completed)
            {
                return Content("This assignment is already completed! Uncheck the status of this assignment before editing!");
            }
            EditAssigmentVM vm = new EditAssigmentVM
            {
                Title = assignment.Title,
                DueDate = assignment.DueDate
            };
            return View(vm);
        }

        // POST: Assignments/Edit/5
        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAssigmentVM assignment, HttpPostedFileBase file, int? id)
        {
            if (ModelState.IsValid)
            {
                Assignment existingAssignment = await db.Assignments.Where(a => a.AssignmentId == id).
                                                                     Include(a => a.TaskFile).
                                                                     FirstOrDefaultAsync();

                if (existingAssignment.Completed)
                {
                    return Content("The assignment is already complete! You cannot edit this assignment.");
                }
                existingAssignment.Title = assignment.Title;
                existingAssignment.DueDate = assignment.DueDate;

                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        TaskFile taskFile = new TaskFile
                        {
                            FileName = file.FileName,
                            UploadFile = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                        };

                        string path = Path.Combine(Server.MapPath("~/Files/Assignments"), taskFile.UploadFile);
                        file.SaveAs(path);

                        if (existingAssignment.TaskFile != null)
                        {
                            DeleteFile(existingAssignment.TaskFile);
                        }

                        existingAssignment.TaskFile.FileName = taskFile.FileName;
                        existingAssignment.TaskFile.UploadFile = taskFile.UploadFile;
                        ViewBag.FileStatus = "File uploaded successfully.";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.FileStatus = "Error while file uploading.";
                    }
                }

                db.Entry(existingAssignment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Student", "Mentors", new { id = existingAssignment.StudentId });
            }
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        [Authorize(Roles = "Mentor")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.Include(a => a.Student).
                                                         FirstOrDefaultAsync(a => a.AssignmentId == id);
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
            Assignment assignment = await db.Assignments.Include(a => a.SubmitFile).
                                                         Include(a => a.TaskFile).
                                                         Include(a => a.Student).
                                                         FirstOrDefaultAsync(a => a.AssignmentId == id);
            var studentId = assignment.StudentId;
            DeleteFile(assignment.TaskFile);
            DeleteFile(assignment.SubmitFile);
            db.Assignments.Remove(assignment);
            await db.SaveChangesAsync();
            return RedirectToAction("Student", "Mentors", new { id = studentId });
        }

        private void DeleteFile(TaskFile file)
        {
            if (file == null) return;

            string fullPath = Path.Combine(Server.MapPath("~/Files/Assignments"), file.UploadFile);
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
