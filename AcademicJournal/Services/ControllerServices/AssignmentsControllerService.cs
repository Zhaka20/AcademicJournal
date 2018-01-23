using AcademicJournal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AcademicJournal.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;
using AcademicJournal.DAL.Context;
using System.IO;
using AcademicJournal.BLL.Services.Abstract;
using Microsoft.AspNet.Identity;
using static System.Net.WebRequestMethods;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DALAbstraction.AbstractRepositories;

namespace AcademicJournal.Services.ControllerServices
{
    public class AssignmentsControllerService : IAssignmentsControllerService
    {
        private IAssignmentRepository repository;
        private ApplicationDbContext db;
        private IMentorService mentorService;
        public AssignmentsControllerService(ApplicationDbContext db, IMentorService mentorService)
        {
            this.db = db;
            this.mentorService = mentorService;
        }


        public async Task<AssignmentsIndexViewModel> GetAssignmentsIndexViewModelAsync()
        {
            List<Assignment> assignments = await db.Assignments.
                                    Include(a => a.AssignmentFile).
                                    Include(a => a.Creator).
                                    Include(a => a.Submissions).
                                    ToListAsync();
            AssignmentsIndexViewModel viewModel = new AssignmentsIndexViewModel
            {
                Assignments = assignments,
                AssignmentModel = new Assignment()
            };
            return viewModel;
        }
        public async Task<AssignmentDetailsViewModel> GetAssignmentsDetailsViewModelAsync(int assignmentId)
        {
            Assignment assignment = await db.Assignments.
                                          Include(a => a.AssignmentFile).
                                          Include(a => a.Creator).
                                          Include(a => a.Submissions).
                                          FirstOrDefaultAsync(s => s.AssignmentId == assignmentId);
            if (assignment == null)
            {
                return null;
            }
            AssignmentDetailsViewModel viewModel = new AssignmentDetailsViewModel
            {
                Assignment = assignment
            };
            return viewModel;
        }
        public async Task<MentorAssignmentsVM> GetMentorAssignmentsViewModelAsync(string mentorId)
        {
            List<Assignment> assignments = await db.Assignments.Where(a => a.CreatorId == mentorId).ToListAsync();
            Mentor mentor = await db.Mentors.FindAsync(mentorId);
            MentorAssignmentsVM viewModel = new MentorAssignmentsVM
            {
                Assignments = assignments,
                Mentor = mentor
            };
            return viewModel;
        }
        public CreateAssigmentVM GetCreateAssignmentViewModel()
        {
            return new CreateAssigmentVM();
        }
        public async Task<int> CreateAssignmentAsync(Controller controller, string mentorId, CreateAssigmentVM inputModel, HttpPostedFileBase file)
        {

            AssignmentFile assignmentFile = new AssignmentFile
            {
                FileName = file.FileName,
                FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
            };
            db.Entry(assignmentFile).State = EntityState.Added;

            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Files/Assignments"), assignmentFile.FileGuid);
            file.SaveAs(path);

            Mentor mentor = await mentorService.GetMentorByIdAsync(mentorId);
            Assignment newAssignment = new Assignment
            {
                Title = inputModel.Title,
                Created = DateTime.Now,
                CreatorId = mentor.Id,
                AssignmentFile = assignmentFile,
            };

            db.Assignments.Add(newAssignment);
            await db.SaveChangesAsync();
            return newAssignment.AssignmentId;
        }
        public async Task<CreateAndAssignToSingleUserVM> GetCreateAndAssignToSingleUserViewModelAsync(string studentId)
        {
            Student student = await db.Students.FindAsync(studentId);
            if (student == null)
            {
                return null;
            }
            CreateAndAssignToSingleUserVM viewModel = new CreateAndAssignToSingleUserVM
            {
                Student = student,
                Title = string.Empty
            };
            return viewModel;
        }
        public async Task<int> CreateAndAssignToSingleUserAsync(Controller controller, string studentId, CreateAssigmentVM inputModel, HttpPostedFileBase file)
        {

            AssignmentFile assignmentFile = new AssignmentFile
            {
                FileName = file.FileName,
                FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
            };
            db.Entry(assignmentFile).State = EntityState.Added;

            string path = Path.Combine(controller.Server.MapPath("~/Files/Assignments"), assignmentFile.FileGuid);
            file.SaveAs(path);

            Mentor mentor = await mentorService.GetMentorByIdAsync(controller.User.Identity.GetUserId());
            Assignment newAssignment = new Assignment
            {
                Title = inputModel.Title,
                Created = DateTime.Now,
                CreatorId = mentor.Id,
                AssignmentFile = assignmentFile,
            };

            db.Assignments.Add(newAssignment);
            await db.SaveChangesAsync();

            Student student = await db.Students.FindAsync(studentId);
            if (student == null)
            {
                throw new Exception();
            }

            Submission newSubmission = new Submission
            {
                StudentId = student.Id,
                AssignmentId = newAssignment.AssignmentId,
                DueDate = DateTime.Now.AddDays(3)
            };

            student.Submissions.Add(newSubmission);

            await db.SaveChangesAsync();
            return newAssignment.AssignmentId;

        }
        public async Task<AssignmentEdtiViewModel> GetAssignmentEdtiViewModelAsync(int assignmentId)
        {
            Assignment assignment = await db.Assignments.FindAsync(assignmentId);
            if (assignment == null)
            {
                return null;
            }
            AssignmentEdtiViewModel viewModel = new AssignmentEdtiViewModel
            {
                Title = assignment.Title,
                AssignmentId = assignment.AssignmentId
            };
            return viewModel;
        }
        public async Task UpdateAssingmentAsync(AssignmentEdtiViewModel inputModel)
        {
            Assignment updatedAssignment = new Assignment
            {
                AssignmentId = inputModel.AssignmentId
            };
            db.Assignments.Attach(updatedAssignment);
            db.Entry(updatedAssignment).Property(a => a.Title).IsModified = true;
            await db.SaveChangesAsync(); ;
        }
        public async Task<DeleteAssigmentViewModel> GetDeleteAssigmentViewModelAsync(int assignmentId)
        {
            Assignment assignment = await db.Assignments.FindAsync(assignmentId);
            if (assignment == null)
            {
                return null;
            }
            DeleteAssigmentViewModel viewModel = new DeleteAssigmentViewModel
            {
                Assignment = assignment
            };
            return viewModel;
        }
        public async Task DeleteAssignmentAsync(int assignmentId)
        {
            Assignment assignment = await db.Assignments.Include(a => a.AssignmentFile).
                                                         Include(a => a.Submissions.Select(s => s.SubmitFile)).
                                                         FirstOrDefaultAsync(a => a.AssignmentId == assignmentId);

            foreach (Submission submission in assignment.Submissions)
            {
                DeleteFile(submission.SubmitFile);
            }
            DeleteFile(assignment.AssignmentFile);

            db.Assignments.Remove(assignment);
            await db.SaveChangesAsync();
        }
        public async Task<AssignmentsRemoveStudentVM> GetAssignmentsRemoveStudentVMAsync(int assignmentId, string studentId)
        {
            Student student = await db.Students.FindAsync(studentId);
            Assignment assignment = await db.Assignments.
                                      Include(a => a.AssignmentFile).
                                      FirstOrDefaultAsync(a => a.AssignmentId == assignmentId);
            AssignmentsRemoveStudentVM viewModel = new AssignmentsRemoveStudentVM
            {
                Assignment = assignment,
                Student = student
            };
            return viewModel;
        }
        public async Task RemoveStudentFromAssignmentAsync(int assignmentId, string studentId)
        {
            Submission submission = await db.Submissions.FindAsync(assignmentId, studentId);
            if (submission != null)
            {
                db.Submissions.Remove(submission);
            }
            await db.SaveChangesAsync();
        }
        public async Task<AssignToStudentVM> GetAssignToStudentViewModelAsync(string studentId)
        {
            Student student = await db.Students.FindAsync(studentId);
            if (student == null)
            {
                return null;
            }

            string mentorId = HttpContext.Current.User.Identity.GetUserId();

            IQueryable<Assignment> assignmentsOfThisMentor = db.Assignments.Include(a => a.AssignmentFile).
                                             Include(a => a.Creator).
                                             Include(a => a.Submissions.Select(s => s.Student)).
                                             Where(a => a.CreatorId == mentorId);

            IQueryable<int> studentsAssignmentIds = db.Submissions.
                                           Where(s => s.StudentId == studentId).
                                           Select(s => s.AssignmentId);

            List<Assignment> notYetAssigned = await assignmentsOfThisMentor.Where(a => !studentsAssignmentIds.Contains(a.AssignmentId)).ToListAsync();

            if (notYetAssigned == null)
            {
                return null;
            }

            AssignToStudentVM viewModel = new AssignToStudentVM
            {
                Assignments = notYetAssigned,
                Student = student,
                AssignmentModel = new Assignment()
            };
            return viewModel;
        }
        public async Task AssignToStudentAsync(string studentId, List<int> assignmentIds)
        {
            if (studentId == null || assignmentIds == null)
            {
                throw new NullReferenceException();
            }

            Student student = await db.Students.FindAsync(studentId);
            if (student == null)
            {
                return;
            }

            List<Assignment> newAssignmentsList = await db.Assignments.
                                              Where(a => assignmentIds.Contains(a.AssignmentId)).
                                              ToListAsync();

            if (newAssignmentsList == null)
            {
                return;
            }

            foreach (Assignment assignment in newAssignmentsList)
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
        }
        public async Task<AssignToStudentsVM> GetAssignToStudentsViewModelAsync(int assigmentId)
        {
            Assignment assignment = await db.Assignments.Include(a => a.AssignmentFile).
                                            Include(a => a.Creator).
                                            Include(a => a.Submissions).
                                            FirstOrDefaultAsync(s => s.AssignmentId == assigmentId);

            if (assignment == null)
            {
                return null;
            }

            IEnumerable<string> assignmedStudentIds = assignment.Submissions.Select(s => s.StudentId);

            List<Student> otherStudents = await db.Students.Where(s => !assignmedStudentIds.Contains(s.Id)).ToListAsync();

            AssignToStudentsVM viewModel = new AssignToStudentsVM
            {
                Assignment = assignment,
                StudentModel = new Student(),
                Students = otherStudents
            };
            return viewModel;
        }
        public async Task AssignToStudentsAsync(int assigmentId, List<string> studentIds)
        {
            if (studentIds == null)
            {
                throw new ArgumentNullException("studentIds");
            }
            Assignment assignment = await db.Assignments.Include(a => a.Creator).
                                                         Include(a => a.AssignmentFile).
                                                         FirstOrDefaultAsync(s => s.AssignmentId == assigmentId);
            if (assignment == null)
            {
                throw new KeyNotFoundException();
            }

            List<Student> students = await db.Students.Where(s => studentIds.Contains(s.Id)).
                                             ToListAsync();

            foreach (Student student in students)
            {
                Submission newSubmission = new Submission
                {
                    StudentId = student.Id,
                    AssignmentId = (int)assigmentId,
                    DueDate = DateTime.Now.AddDays(3)
                };

                student.Submissions.Add(newSubmission);
            }

            await db.SaveChangesAsync();
        }
        public async Task<IFileStreamWithInfo> GetAssignmentFileAsync(int assignmentId)
        {
            Assignment assignment = await db.Assignments.FindAsync(assignmentId);
            if (assignment == null || assignment.AssignmentFile == null)
            {
                return null;
            }
            string origFileName = assignment.AssignmentFile.FileName;
            string fileGuid = assignment.AssignmentFile.FileGuid;
            string mimeType = MimeMapping.GetMimeMapping(origFileName);
            IFileStreamWithInfo fileStream = null;
            try
            {
                string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Files/Assignments"), fileGuid);
                fileStream = new FileStreamWithInfo
                {
                    FileStream = System.IO.File.ReadAllBytes(filePath),
                    FileName = origFileName,
                    FileType = mimeType
                };
            }
            catch (Exception ex)
            {
                return null;
            }
            return fileStream;
        }
        public void Dispose()
        {
            db.Dispose();
            mentorService.Dispose();
        }

        private void DeleteFile(DataModel.Models.FileInfo file)
        {
            if (file == null) return;

            string fullPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Files/Assignments"), file.FileGuid);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        public async Task<StudentsAndSubmissionsListVM> GetStudentsAndSubmissionsListVMAsync(int assingmentId)
        {
            Assignment assignment = await db.Assignments.
                                             Include(a => a.AssignmentFile).
                                             Include(a => a.Creator).
                                             Include(a => a.Submissions.Select(s => s.Student)).
                                             FirstOrDefaultAsync(s => s.AssignmentId == assingmentId);

            if (assignment == null)
            {
                return null;
            }
            StudentsAndSubmissionsListVM viewModel = new StudentsAndSubmissionsListVM
            {
                Assignment = assignment,
                StudentModel = new Student(),
                Submissions = assignment.Submissions

            };
            return viewModel;
        }

        public class FileStreamWithInfo : IFileStreamWithInfo
        {
            public string FileName { get; set; }
            public byte[] FileStream { get; set; }
            public string FileType { get; set; }
        }
    }
}