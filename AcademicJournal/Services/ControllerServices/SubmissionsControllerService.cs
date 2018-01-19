using AcademicJournal.Services.Abstractions;
using System;
using System.Linq;
using System.Data.Entity;
using System.Web;
using AcademicJournal.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;
using AcademicJournal.DAL.Context;
using AcademicJournal.DAL.Models;
using System.IO;
using System.Net.Http;

namespace AcademicJournal.Services.ControllerServices
{
    public class SubmissionsControllerService : ISubmissionsControllerService
    {
        private ApplicationDbContext db;

        public SubmissionsControllerService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<SubmissionsIndexVM> GetSubmissionsIndexViewModelAsync()
        {
            System.Collections.Generic.List<Submission> submissions = await db.Submissions.
                                    Include(s => s.Assignment).
                                    Include(s => s.Student).
                                    Include(s => s.SubmitFile).
                                    ToListAsync();
            SubmissionsIndexVM viewModel = new SubmissionsIndexVM
            {
                AssignmentModel = new Assignment(),
                SubmissionModel = new Submission(),
                Submissions = submissions
            };
            return viewModel;

        }

        public async Task<AssignmentSumbissionsVM> GetAssignmentSubmissionsViewModelAsync(int assignmentId)
        {
            Assignment assignment = await db.Assignments.Include(a => a.AssignmentFile).
                                                        Include(a => a.Creator).
                                                        Include(a => a.Submissions.Select(s => s.Student)).
                                                        Include(a => a.Submissions.Select(s => s.SubmitFile)).
                                                        FirstOrDefaultAsync(a => a.AssignmentId == assignmentId);
            if (assignment == null)
            {
                return null;
            }

            AssignmentSumbissionsVM viewModel = new AssignmentSumbissionsVM
            {
                Assignment = assignment,
                Submissions = assignment.Submissions,
                SubmissionModel = new Submission(),
                StudentModel = new Student()
            };
            return viewModel;
        }

        public async Task<SubmissionDetailsVM> GetSubmissionDetailsViewModelAsync(int assignmentId, string studentId)
        {
            Submission submission = await db.Submissions.FindAsync(assignmentId, studentId);
            if (submission == null)
            {
                return null;
            }
            SubmissionDetailsVM viewModel = new SubmissionDetailsVM
            {
                Submission = submission,
                AssignmentModel = new Assignment(),
                StudentModel = new Student()
            };
            return viewModel;
        }

        public async Task<EditSubmissionVM> GetEditSubmissionViewModelAsync(int assignmentId, string studentId)
        {
            Submission submission = await db.Submissions.FindAsync(assignmentId, studentId);
            if (submission == null)
            {
                return null;
            }

            EditSubmissionVM viewModel = new EditSubmissionVM
            {
                StudentId = studentId,
                AssignmentId = assignmentId,
                Completed = submission.Completed,
                DueDate = submission.DueDate,
                Grade = (int)submission.Grade
            };
            return viewModel;
        }

        public async Task UpdateSubmissionAsync(EditSubmissionVM viewModel)
        {
            Submission editedSubmission = new Submission
            {
                AssignmentId = viewModel.AssignmentId,
                StudentId = viewModel.StudentId,
                Grade = (byte?)viewModel.Grade,
                DueDate = viewModel.DueDate,
                Completed = viewModel.Completed
            };
            db.Submissions.Attach(editedSubmission);
            db.Entry(editedSubmission).Property(s => s.DueDate).IsModified = true;
            db.Entry(editedSubmission).Property(s => s.Completed).IsModified = true;
            db.Entry(editedSubmission).Property(s => s.Grade).IsModified = true;
            await db.SaveChangesAsync();
        }

        public async Task<DeleteSubmissionVM> GetDeleteSubmissionViewModelAsync(int assignmentId, string studentId)
        {
            Submission submission = await db.Submissions.FindAsync(assignmentId, studentId);
            if (submission == null)
            {
                return null;
            }
            DeleteSubmissionVM viewModel = new DeleteSubmissionVM
            {
                AssignmentModel = new Assignment(),
                StudentModel = new Student(),
                Submission = submission
            };
            return viewModel;
        }

        public async Task DeleteSubmissionAsync(int assignmentId, string studentId)
        {
            Submission submission = await db.Submissions.FindAsync(assignmentId, studentId);
            if (submission.SubmitFile != null)
            {
                DeleteFile(submission.SubmitFile);
            }
            db.Submissions.Remove(submission);
            await db.SaveChangesAsync();
        }

        public async Task<IFileStreamWithInfo> GetSubmissionFileAsync(Controller controller, int assignmentId, string studentId)
        {
            Submission submission = await db.Submissions.FindAsync(assignmentId, studentId);
            if(submission == null || submission.SubmitFile == null)
            {
                return null;
            }
            string origFileName = submission.SubmitFile.FileName;
            string fileGuid = submission.SubmitFile.FileGuid;
            string mimeType = MimeMapping.GetMimeMapping(origFileName);
            IFileStreamWithInfo fileStream = null;
            try
            {
                string filePath = Path.Combine(controller.Server.MapPath("~/Files/Assignments"), fileGuid);
                fileStream = new FileStreamWithInfo
                {
                    FileStream = File.ReadAllBytes(filePath),
                    FileName = origFileName,
                    FileType = mimeType
                };
            }
            catch
            {
                return null;
            }          
            return fileStream;
        }

        public async Task<bool> ToggleSubmissionCompleteStatusAsync(int assignmentId, string studentId)
        {
            Submission submission = await db.Submissions.FindAsync(assignmentId, studentId);
            if (submission == null)
            {
                throw new Exception();
            }
            submission.Completed = submission.Completed == true ? false : true;
            await db.SaveChangesAsync();
            return submission.Completed;
        }

        public async Task<EvaluateSubmissionVM> GetSubmissionEvaluateViewModelAsync(int assignmentId, string studentId)
        {
            Submission submission = await db.Submissions.FindAsync(assignmentId, studentId);
            if (submission == null)
            {
                return null;
            }
            EvaluateSubmissionVM viewModel = new EvaluateSubmissionVM
            {
                Submission = submission,
                Grade = (submission.Grade ?? 0),
                assignmentId = assignmentId,
                studentId = studentId
            };
            return viewModel;
        }

        public async Task EvaluateSubmissionAsync(EvaluateSubmissionInputModel inputModel)
        {
            Submission submission = await db.Submissions.FindAsync(inputModel.assignmentId, inputModel.studentId);
            if (submission == null)
            {
                throw new Exception();
            }

            submission.Grade = (byte)inputModel.Grade;
            await db.SaveChangesAsync();
        }

        public async Task UploadFileAsync(Controller controller, HttpPostedFileBase file, int assignmentId,string studentId)
        {
            Submission submission = await db.Submissions.FindAsync(assignmentId, studentId);
            if (submission == null)
            {
                throw new Exception();
            }
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    if (submission.Completed)
                    {
                        controller.ViewBag.FileStatus = "This assignment is already complete!You cannot updload a file to this assignment.";
                        return;
                    }

                    SubmitFile newSubmitFile = new SubmitFile
                    {
                        FileName = file.FileName,
                        FileGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)
                    };

                    string path = Path.Combine(controller.Server.MapPath("~/Files/Assignments"), newSubmitFile.FileGuid);
                    file.SaveAs(path);

                    if (submission.SubmitFile != null)
                    {
                        DeleteFile(submission.SubmitFile);
                        db.SubmitFiles.Remove(submission.SubmitFile);
                    }

                    submission.SubmitFile = newSubmitFile;
                    submission.Submitted = DateTime.Now;

                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    controller.ViewBag.FileStatus = "Error while file uploading.";
                }
            }
            else
            {
                controller.ModelState.AddModelError("", "Upload file is not selected!");
            }
            return;
        }

        private void DeleteFile(DAL.Models.FileInfo file)
        {
            if (file == null) return;

            string fullPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Files/Assignments"), file.FileGuid);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<Submission> GetSubmissionAsync(int assignmentId, string studentId)
        {
            Submission submission = await db.Submissions.FindAsync(assignmentId, studentId);
            return submission;
        }
    }

    public class FileStreamWithInfo : IFileStreamWithInfo
    {
        public string FileName { get; set; }
        public byte[] FileStream { get; set; }
        public string FileType  { get; set; }
    }

}