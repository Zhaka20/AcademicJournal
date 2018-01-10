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
            return View(await db.Assignments.Include(a => a.Students).ToListAsync());
        }

        public async Task<FileResult> DownloadTaskFile(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var assignment = await db.Assignments.FindAsync(id);

            string origFileName = assignment.TaskFile.FileName;
            string fileGuid = assignment.TaskFile.FileGuid;
            string filePath = Path.Combine(Server.MapPath("~/Files/Assignments"), fileGuid);
            string mimeType = MimeMapping.GetMimeMapping(origFileName);
            return File(filePath,mimeType, origFileName);
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
                            FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                        };

                        string path = Path.Combine(Server.MapPath("~/Files/Assignments"), taskFile.FileGuid);
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

        [HttpGet]
        public async Task<ActionResult> CreateGroupAssignment(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var students = await db.Students.Where(s => s.MentorId == id).ToListAsync();
            CreateGroupAssignmentVM vm = new CreateGroupAssignmentVM
            {
                Students = students,
                Assignment = new CreateAssigmentVM(),
                Student = new Student()
            };
            return View(vm);
        }

        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateGroupAssignment(/*CreateAssigmentVM assignment, HttpPostedFileBase file, List<string> studentId*/ CreateGroupAssignmentVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.file != null && model.file.ContentLength > 0)

                {
                    try
                    {
                        var mentorId = User.Identity.GetUserId();
                        TaskFile taskFile = new TaskFile
                        {
                            FileName = model.file.FileName,
                            FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(model.file.FileName)
                        };

                        string path = Path.Combine(Server.MapPath("~/Files/Assignments"), taskFile.FileGuid);
                        model.file.SaveAs(path);
                        db.Entry(taskFile).State = EntityState.Added;

                        var students = await db.Students.Where(s => model.studentId.Contains(s.Id)).ToListAsync();
                       
                        foreach ( Student student in students)
                        {
                            Assignment newAssignment = new Assignment
                            {
                                Title = model.Assignment.Title,
                                Created = DateTime.Now,
                                CreatorId = mentorId,
                                DueDate = model.Assignment.DueDate,
                                TaskFile = taskFile,
                            };
                            student.Assignments.Add(newAssignment);
                        }

                        await db.SaveChangesAsync();

                        ViewBag.FileStatus = "File uploaded successfully.";
                        return RedirectToAction("Home", "Mentors", new { id = mentorId });
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
            return View(model);
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
                            FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                        };

                        string path = Path.Combine(Server.MapPath("~/Files/Assignments"), taskFile.FileGuid);
                        file.SaveAs(path);

                        if (existingAssignment.TaskFile != null)
                        {
                            DeleteFile(existingAssignment.TaskFile);
                        }

                        existingAssignment.TaskFile.FileName = taskFile.FileName;
                        existingAssignment.TaskFile.FileGuid = taskFile.FileGuid;
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
            Assignment assignment = await db.Assignments.Include(a => a.Submissions).
                                                         Include(a => a.TaskFile).
                                                         Include(a => a.).
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
