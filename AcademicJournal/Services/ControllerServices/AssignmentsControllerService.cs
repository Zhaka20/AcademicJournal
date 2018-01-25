using AcademicJournal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
using AcademicJournal.BLL.Services.Abstract;
using Microsoft.AspNet.Identity;
using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Assignments;

namespace AcademicJournal.Services.ControllerServices
{
    public class AssignmentsControllerService : IAssignmentsControllerService
    {
        protected readonly IAssignmentService service;
        protected readonly IMentorService mentorService;
        protected readonly IStudentService studentService;
        protected readonly IAssignmentFileService fileService;
        protected readonly ISubmissionService submissionService;

        //private IAssignmentRepository repository;
        //private ApplicationDbContext db;
        //private IMentorService mentorService;
        //public AssignmentsControllerService(ApplicationDbContext db, IMentorService mentorService)
        //{
        //    this.db = db;
        //    this.mentorService = mentorService;
        //}

        public AssignmentsControllerService(IAssignmentService service, IMentorService mentorService, IStudentService studentService, IAssignmentFileService fileService, ISubmissionService submissionService)
        {
            this.service = service;
            this.mentorService = mentorService;
            this.studentService = studentService;
            this.fileService = fileService;
            this.submissionService = submissionService;
        }


        public async Task<IndexViewModel> GetAssignmentsIndexViewModelAsync()
        {
            IEnumerable<Assignment> assignments = await service.GetAllAsync(null, null, null, null,
                                                                            a => a.AssignmentFile,
                                                                            a => a.Creator,
                                                                            a => a.Submissions);

            IndexViewModel viewModel = new IndexViewModel
            {
                Assignments = assignments,
                AssignmentModel = new Assignment()
            };
            return viewModel;
        }
        public async Task<DetailsViewModel> GetAssignmentsDetailsViewModelAsync(int assignmentId)
        {
            Assignment assignment = await service.GetFirstOrDefaultAsync(s => s.AssignmentId == assignmentId,
                                                                              s => s.AssignmentFile,
                                                                              s => s.Creator,
                                                                              s => s.Submissions);
                                        
            if (assignment == null)
            {
                return null;
            }
            DetailsViewModel viewModel = new DetailsViewModel
            {
                Assignment = assignment
            };
            return viewModel;
        }
        public async Task<MentorViewModel> GetMentorAssignmentsViewModelAsync(string mentorId)
        {
            IEnumerable<Assignment> assignments = await service.GetAllAsync(a => a.CreatorId == mentorId);
            Mentor mentor = await mentorService.GetByIdAsync(mentorId);
            MentorViewModel viewModel = new MentorViewModel
            {
                Assignments = assignments,
                Mentor = mentor
            };
            return viewModel;
        }
        public CreateViewModel GetCreateAssignmentViewModel()
        {
            return new CreateViewModel();
        }
        public async Task<int> CreateAssignmentAsync(Controller controller, string mentorId, CreateViewModel inputModel, HttpPostedFileBase file)
        {

            AssignmentFile assignmentFile = new AssignmentFile
            {
                FileName = file.FileName,
                FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
            };
            fileService.Create(assignmentFile);

            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Files/Assignments"), assignmentFile.FileGuid);
            file.SaveAs(path);

            Mentor mentor = await mentorService.GetByIdAsync(mentorId);
            Assignment newAssignment = new Assignment
            {
                Title = inputModel.Title,
                Created = DateTime.Now,
                CreatorId = mentor.Id,
                AssignmentFile = assignmentFile,
            };
            //fileService.SaveChanges();
            service.Create(newAssignment);
            await service.SaveChangesAsync();
            return newAssignment.AssignmentId;
        }
        public async Task<CreateAndAssignToSingleUserViewModel> GetCreateAndAssignToSingleUserViewModelAsync(string studentId)
        {
            Student student = await studentService.GetStudentByEmailAsync(studentId);
            if (student == null)
            {
                return null;
            }
            CreateAndAssignToSingleUserViewModel viewModel = new CreateAndAssignToSingleUserViewModel
            {
                Student = student,
                Title = string.Empty
            };
            return viewModel;
        }
        public async Task<int> CreateAndAssignToSingleUserAsync(Controller controller, string studentId, CreateViewModel inputModel, HttpPostedFileBase file)
        {

            AssignmentFile assignmentFile = new AssignmentFile
            {
                FileName = file.FileName,
                FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
            };
            fileService.Create(assignmentFile);

            string path = Path.Combine(controller.Server.MapPath("~/Files/Assignments"), assignmentFile.FileGuid);
            file.SaveAs(path);

            string mentorId = controller.User.Identity.GetUserId();
            Mentor mentor = await mentorService.GetByIdAsync(mentorId);
            Assignment newAssignment = new Assignment
            {
                Title = inputModel.Title,
                Created = DateTime.Now,
                CreatorId = mentor.Id,
                AssignmentFile = assignmentFile,
            };

            service.Create(newAssignment);
            await service.SaveChangesAsync();

            Student student = await studentService.GetByIdAsync(studentId);
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

            await service.SaveChangesAsync();
            return newAssignment.AssignmentId;

        }
        public async Task<EdtiViewModel> GetAssignmentEdtiViewModelAsync(int assignmentId)
        {
            Assignment assignment = await service.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                return null;
            }
            EdtiViewModel viewModel = new EdtiViewModel
            {
                Title = assignment.Title,
                AssignmentId = assignment.AssignmentId
            };
            return viewModel;
        }
        public async Task UpdateAssingmentAsync(EdtiViewModel inputModel)
        {
            Assignment updatedAssignment = new Assignment
            {
                AssignmentId = inputModel.AssignmentId
            };
            service.Update(updatedAssignment, a => a.Title);           
            await service.SaveChangesAsync(); ;
        }
        public async Task<DeleteViewModel> GetDeleteAssigmentViewModelAsync(int assignmentId)
        {
            Assignment assignment = await service.GetByIdAsync(assignmentId);
            if (assignment == null)
            {
                return null;
            }
            DeleteViewModel viewModel = new DeleteViewModel
            {
                Assignment = assignment
            };
            return viewModel;
        }
        public async Task DeleteAssignmentAsync(int assignmentId)
        {
            Assignment assignment = await service.GetFirstOrDefaultAsync(a => a.AssignmentId == assignmentId,
                                                                         a => a.AssignmentFile,
                                                                         a => a.Submissions.Select(s => s.SubmitFile));

            foreach (Submission submission in assignment.Submissions)
            {
                DeleteFile(submission.SubmitFile);
            }
            DeleteFile(assignment.AssignmentFile);

            service.Delete(assignment);
            await service.SaveChangesAsync();
        }
        public async Task<RemoveStudentViewModel> GetAssignmentsRemoveStudentVMAsync(int assignmentId, string studentId)
        {
            Student student = await studentService.GetByIdAsync(studentId);
            Assignment assignment = await service.GetFirstOrDefaultAsync(a => a.AssignmentId == assignmentId,
                                                                         a => a.AssignmentFile);

            RemoveStudentViewModel viewModel = new RemoveStudentViewModel
            {
                Assignment = assignment,
                Student = student
            };
            return viewModel;
        }
        public async Task RemoveStudentFromAssignmentAsync(int assignmentId, string studentId)
        {

            Submission submission = await submissionService.GetByCompositeKeysAsync(assignmentId, studentId);
            if (submission != null)
            {
                submissionService.Delete(submission);
            }
            await submissionService.SaveChangesAsync();
        }
        public async Task<AssignToStudentViewModel> GetAssignToStudentViewModelAsync(string studentId)
        {
            Student student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }

            string mentorId = HttpContext.Current.User.Identity.GetUserId();

            IEnumerable<Assignment> assignmentsOfThisMentor = await service.GetAllAsync(a => a.CreatorId == mentorId, null,null,null,
                                                                                  a => a.Creator,
                                                                                  a => a.Submissions.Select(s => s.Student));

            IEnumerable<Submission> studentsSubmissions = await submissionService.GetAllAsync(s => s.StudentId == studentId);
            var studentsAssignmentIds = studentsSubmissions.Select(s => s.AssignmentId);
                        
            IEnumerable<Assignment> notYetAssigned = assignmentsOfThisMentor.Where(a => !studentsAssignmentIds.Contains(a.AssignmentId));

            if (notYetAssigned == null)
            {
                return null;
            }

            AssignToStudentViewModel viewModel = new AssignToStudentViewModel
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
                throw new ArgumentNullException();
            }

            Student student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                return;
            }

            IEnumerable<Assignment> newAssignmentsList = await service.GetAllAsync(a => assignmentIds.Contains(a.AssignmentId));
 
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

            await service.SaveChangesAsync();
        }
        public async Task<AssignToStudentsViewModel> GetAssignToStudentsViewModelAsync(int assigmentId)
        {
            Assignment assignment = await service.GetFirstOrDefaultAsync(s => s.AssignmentId == assigmentId,
                                                                         a => a.Creator,
                                                                         a => a.Submissions);
               
            if (assignment == null)
            {
                return null;
            }

            IEnumerable<string> assignedStudentIds = assignment.Submissions.Select(s => s.StudentId);

            IEnumerable<Student> otherStudents = await studentService.GetAllAsync(s => !assignedStudentIds.Contains(s.Id));

            AssignToStudentsViewModel viewModel = new AssignToStudentsViewModel
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
            Assignment assignment = await service.GetFirstOrDefaultAsync(s => s.AssignmentId == assigmentId,
                                                                   a => a.AssignmentFile);
            if (assignment == null)
            {
                throw new KeyNotFoundException();
            }

            IEnumerable<Student> students = await studentService.GetAllAsync(s => studentIds.Contains(s.Id));

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

            await service.SaveChangesAsync();
        }
        public async Task<IFileStreamWithInfo> GetAssignmentFileAsync(int assignmentId)
        {
            Assignment assignment = await service.GetByIdAsync(assignmentId);
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
            catch (Exception )
            {
                return null;
            }
            return fileStream;
        }
        public void Dispose()
        {
            IDisposable dispose = studentService as IDisposable;
            dispose = studentService as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }

            dispose = mentorService as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }

            dispose = fileService as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }

            dispose = service as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }
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

        public async Task<StudentsAndSubmissionsListViewModel> GetStudentsAndSubmissionsListVMAsync(int assingmentId)
        {
            Assignment assignment = await service.GetFirstOrDefaultAsync(s => s.AssignmentId == assingmentId,
                                                                         a => a.AssignmentFile,  
                                                                         a => a.Creator,         
                                                                         a => a.Submissions.Select(s => s.Student));
                                            
            if (assignment == null)
            {
                return null;
            }
            StudentsAndSubmissionsListViewModel viewModel = new StudentsAndSubmissionsListViewModel
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