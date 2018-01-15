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
using AcademicJournal.BLL.Services.Abstract;

namespace AcademicJournal.Controllers
{
    public class AssignmentsController : Controller
    {
        private ApplicationDbContext db;
        private IMentorService mentorService;
        public AssignmentsController(ApplicationDbContext db, IMentorService mentorService)
        {
            this.mentorService = mentorService;
            this.db = db;
        }

        // GET: Assignments
        public async Task<ActionResult> Index()
        {
            var assignments = db.Assignments.Include(a => a.AssignmentFile).Include(a => a.Creator).Include(a => a.Submissions);
            return View(await assignments.ToListAsync());
        }

        // GET: Assignments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.Include(a => a.AssignmentFile).Include(a => a.Creator).Include(a => a.Submissions).FirstOrDefaultAsync(s => s.AssignmentId == id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        public async Task<ActionResult> Mentor(string id)
        {
            var assignments = await db.Assignments.Where(a => a.CreatorId == id).ToListAsync();
            var mentor = await db.Mentors.FindAsync(id);
            MentorAssignmentsVM vm = new MentorAssignmentsVM
            {
                Assignments = assignments,
                Mentor = mentor
            };
            return View(vm);
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


        // GET: Assignments/Create
        [Authorize(Roles = "Mentor")]
        public ActionResult CreateAndAssignToSingleUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAndAssignToSingleUser(CreateAssigmentVM inputModel, HttpPostedFileBase file, string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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

                        //Assign to given user

                        var student = await db.Students.FindAsync(id);
                        if (student == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                        }

                        Submission newSubmission = new Submission
                        {
                            StudentId = student.Id,
                            AssignmentId = newAssignment.AssignmentId,
                            DueDate = DateTime.Now.AddDays(3)
                        };

                        student.Submissions.Add(newSubmission);

                        await db.SaveChangesAsync();
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
            Assignment assignment = await db.Assignments.Include(a => a.AssignmentFile).
                                                         Include(a => a.Submissions.Select(s => s.SubmitFile)).
                                                         FirstOrDefaultAsync(a => a.AssignmentId == id);

            foreach (Submission submission in assignment.Submissions)
            {
                DeleteFile(submission.SubmitFile);
            }
            DeleteFile(assignment.AssignmentFile);

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
        public async Task<ActionResult> Submissions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.Include(a => a.AssignmentFile).
                                                         Include(a => a.Creator).
                                                         Include(a => a.Submissions.Select(s => s.Student)).
                                                         FirstOrDefaultAsync(s => s.AssignmentId == id);

            if (assignment == null)
            {
                return HttpNotFound();
            }
            StudentsAndSubmissionsListVM vm = new StudentsAndSubmissionsListVM
            {
                Assignment = assignment,
                StudentModel = new Student(),
                Submissions = assignment.Submissions

            };
            return View(vm);
        }

        [Authorize(Roles = "Mentor")]
        [Route("assignments/removestudent/{id:int}/{studentId}")]
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

        [ActionName("RemoveStudent")]
        [Authorize(Roles = "Mentor")]
        [Route("assignments/removestudent/{id:int}/{studentId}")]
        [HttpPost]
        public async Task<ActionResult> RemoveStudentPost(int id, string studentId)
        {
            var submission = await db.Submissions.FindAsync(id, studentId);
            if (submission != null)
            {
                db.Submissions.Remove(submission);
            }
            await db.SaveChangesAsync();
            return RedirectToAction("StudentsAndSubmissionsList", new { id = id });
        }

        public async Task<ActionResult> AssignToStudent(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = await db.Students.FindAsync(id);
            if(student == null)
            {
                return HttpNotFound();
            }

            var mentorId = User.Identity.GetUserId();

            var assignmentsOfThisMentor = db.Assignments.Include(a => a.AssignmentFile).
                                             Include(a => a.Creator).
                                             Include(a => a.Submissions.Select(s => s.Student)).
                                             Where(a => a.CreatorId == mentorId);

            var studentsAssignmentIds = db.Submissions.Where(s => s.StudentId == id).Select(s => s.AssignmentId);

            var notYetAssigned = await assignmentsOfThisMentor.Where(a => !studentsAssignmentIds.Contains(a.AssignmentId)).ToListAsync();
                       
            if (notYetAssigned == null)
            {
                return HttpNotFound();
            }

            var vm = new AssignToStudentVM
            {
                Assignments = notYetAssigned,
                Student = student,
                AssignmentModel = new Assignment()
            };
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignToStudent(string id, List<int> assignmentIds)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if(assignmentIds == null)
            {
                ViewBag.ErrorMessage = "No assignment is selected!";
                return RedirectToAction("AssignToStudent", new { id = id });
            }

            var student = await db.Students.FindAsync(id);
            if(student == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var newAssignmentsList = await db.Assignments.Where(a => assignmentIds.Contains(a.AssignmentId)).ToListAsync();

            if (newAssignmentsList == null)
            {
                return HttpNotFound();
            }

            foreach (var assignment in newAssignmentsList)
            {
                Submission newSubmission = new Submission
                {
                    StudentId = student.Id,
                    AssignmentId = assignment.AssignmentId,
                    DueDate = DateTime.Now.AddDays(3)
                };
                assignment.Submissions.Add(newSubmission);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Student", "Mentors", new { id = id });
        }

        public async Task<ActionResult> AssignToStudents(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = await db.Assignments.Include(a => a.AssignmentFile).
                                             Include(a => a.Creator).
                                             Include(a => a.Submissions).
                                             FirstOrDefaultAsync(s => s.AssignmentId == id);

            if (assignment == null)
            {
                return HttpNotFound();
            }

            var assignmedStudentIds = assignment.Submissions.Select(s => s.StudentId);

            var otherStudents = await db.Students.Where(s => !assignmedStudentIds.Contains(s.Id)).ToListAsync();

            AssignToStudentsVM vm = new AssignToStudentsVM
            {
                Assignment = assignment,
                StudentModel = new Student(),
                Students = otherStudents
            };
            return View(vm);
        }

        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignToStudents(int? id, List<string> studentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Assignment assignment = await db.Assignments.Include(a => a.Creator).
                                                         Include(a => a.AssignmentFile).
                                                         FirstOrDefaultAsync(s => s.AssignmentId == id);
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
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Assignment", "Submissions", new { id = id });
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
