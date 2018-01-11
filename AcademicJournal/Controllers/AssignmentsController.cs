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
using Microsoft.AspNet.Identity;

namespace AcademicJournal.Controllers
{
    public class AssignmentsController : Controller
    {
        private ApplicationDbContext db;
        private MentorService mentorService;
        public AssignmentsController(ApplicationDbContext db, MentorService mentorService)
        {
            this.mentorService = mentorService;
            this.db = db;
        }

        // GET: Assignments
        public async Task<ActionResult> Index()
        {
            var assignments = db.Assignments.Include(a => a.AssignmentFile).Include(a => a.Creator).Include(a => a.Students);
            return View(await assignments.ToListAsync());
        }

        // GET: Assignments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.Include(a => a.AssignmentFile).Include(a => a.Creator).Include(a => a.Students).FirstOrDefaultAsync(s => s.AssignmentId == id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }



        // GET: Assignments/Create
        [Authorize(Roles = "Mentor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAssigmentVM inputModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        AssignmentFile assignmentFile = new AssignmentFile
                        {
                            FileName = file.FileName,
                            FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                        };
                        db.Entry(assignmentFile).State = EntityState.Added;

                        string path = Path.Combine(Server.MapPath("~/Files/Assignments"), assignmentFile.FileGuid);
                        file.SaveAs(path);

                        Mentor mentor = await mentorService.GetMentorByIdAsync(User.Identity.GetUserId());
                        Assignment newAssignment = new Assignment
                        {
                            Title = inputModel.Title,
                            Created = DateTime.Now,
                            CreatorId = mentor.Id,
                            AssignmentFile = assignmentFile,
                        };

                        db.Assignments.Add(newAssignment);
                        await db.SaveChangesAsync();

                        ViewBag.FileStatus = "File uploaded successfully.";
                        return RedirectToAction("Details", "Assignments", new { id = newAssignment.AssignmentId });
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
            return View(inputModel);
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
            ViewBag.AssignmentFileId = new SelectList(db.AssignmentFiles, "Id", "FileGuid", assignment.AssignmentFileId);
            ViewBag.CreatorId = new SelectList(db.Users, "Id", "FirstName", assignment.CreatorId);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AssignmentId,Title,AssignmentFileId,CreatorId,Created")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AssignmentFileId = new SelectList(db.AssignmentFiles, "Id", "FileGuid", assignment.AssignmentFileId);
            ViewBag.CreatorId = new SelectList(db.Users, "Id", "FirstName", assignment.CreatorId);
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


        public async Task<FileResult> DownloadAssignmentFile(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var assignment = await db.Assignments.FindAsync(id);

            string origFileName = assignment.AssignmentFile.FileName;
            string fileGuid = assignment.AssignmentFile.FileGuid;
            string filePath = Path.Combine(Server.MapPath("~/Files/Assignments"), fileGuid);
            string mimeType = MimeMapping.GetMimeMapping(origFileName);
            return File(filePath, mimeType, origFileName);
        }
        public async Task<ActionResult> StudentsAndSubmissionsList(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.Include(a => a.AssignmentFile).Include(a => a.Creator).Include(a => a.Students).FirstOrDefaultAsync(s => s.AssignmentId == id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            StudentsAndSubmissionsListVM vm = new StudentsAndSubmissionsListVM
            {
                Assignment = assignment,
                StudentModel = new Student(),
                Students = assignment.Students,

            };
            return View(vm);
        }

        [Authorize(Roles = "Mentor")]
        [Route("assignments/removestudent/{id:int}/{studentId:string}")]
        public async Task<ActionResult> RemoveStudent(int id, string studentId)
        {
            var student = await db.Students.FindAsync(studentId);
            var assignment = await db.Assignments.Include(a => a.AssignmentFile).FirstOrDefaultAsync(a => a.AssignmentId == id);
            RemoveStudentVM vm = new RemoveStudentVM
            {
                Assignment = assignment,
                Student = student
            };
            return View(vm);
        }

        //[ActionName("RemoveStudent")]
        //[Authorize(Roles = "Mentor")]
        //[Route("assignments/removestudent/{id:int}/{studentId:string}")]
        //[HttpPost]
        //public async Task<ActionResult> RemoveStudentPost(int id, string studentId)
        //{
        //    var assignment = await db.Assignments.FindAsync(id);
        //    var student = await db.Students.FindAsync(studentId);
        //    var submission = await db.Submissions.FindAsync()
        //    if (assignment != null && student != null)
        //    {
        //        assignment.Students.Remove(student);
        //        db.Submissions.Remove();
        //    }
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("StudentsAndSubmissionsList", new { id = id });
        //}

        public async Task<ActionResult> AssignToStudents(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Assignment assignment = await db.Assignments.Include(a => a.AssignmentFile).Include(a => a.Creator).Include(a => a.Students).FirstOrDefaultAsync(s => s.AssignmentId == id);
            var otherStudents = await db.Students.Except(assignment.Students).ToListAsync();
            if (assignment == null)
            {
                return HttpNotFound();
            }

            AssignToStudentsVM vm = new AssignToStudentsVM
            {
                Assignment = assignment,
                StudentModel = new Student(),
                Students = otherStudents
            };
            return View(vm);

        }

        [Authorize(Roles ="Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignToStudents(int? id, List<string> studentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Assignment assignment = await db.Assignments.Include(a => a.Creator).Include(a => a.Students).FirstOrDefaultAsync(s => s.AssignmentId == id);
            if (assignment == null)
            {
                return HttpNotFound();
            }

            if (studentId == null)
            {
                ViewBag.ErrorMessage = "No student is selected!";
                return RedirectToAction("AssignToStudents", new { id = id });
            }

            var students = await db.Students.Where(s => studentId.Contains(s.Id)).ToListAsync();

            foreach (Student student in students)
            {
                Submission newSubmission = new Submission
                {
                    StudentId = student.Id,
                    AssignmentId = (int)id,
                    DueDate = DateTime.Now.AddDays(3)
                };
                student.Submissions.Add(newSubmission);
                assignment.Submissions.Add(newSubmission);
                assignment.Students.Add(student);
            }
            
            await db.SaveChangesAsync();
            return RedirectToRoute("StudentsAndSubmissionsList", new { id = id });
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
