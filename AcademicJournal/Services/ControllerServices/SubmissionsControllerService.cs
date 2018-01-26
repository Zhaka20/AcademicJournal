using AcademicJournal.Services.Abstractions;
using System;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
using AcademicJournal.DataModel.Models;
using AcademicJournal.AbstractBLL.AbstractServices;
using System.Collections.Generic;
using AcademicJournal.ViewModels.Submissions;

namespace AcademicJournal.Services.ControllerServices
{
    public class SubmissionsControllerService : ISubmissionsControllerService
    {
        protected readonly ISubmissionService service;
        protected readonly IAssignmentService assigmentService;

        public SubmissionsControllerService(ISubmissionService service, IAssignmentService assignmentService)
        {
            this.service = service;
            this.assigmentService = assignmentService;
        }
        public async Task<SubmissionsIndexViewModel> GetSubmissionsIndexViewModelAsync()
        {
            IEnumerable<Submission> submissions = await service.GetAllAsync(null, null, null, null,
                                                                            s => s.Assignment,
                                                                            s => s.Student,
                                                                            s => s.SubmitFile);
            SubmissionsIndexViewModel viewModel = new SubmissionsIndexViewModel
            {
                AssignmentModel = new Assignment(),
                SubmissionModel = new Submission(),
                Submissions = submissions
            };
            return viewModel;
        }

        public async Task<AssignmentViewModel> GetAssignmentSubmissionsViewModelAsync(int assignmentId)
        {
            Assignment assignment = await assigmentService.GetFirstOrDefaultAsync(a => a.AssignmentId == assignmentId,
                                                                                  a => a.Creator,
                                                                                  a => a.Submissions.Select(s => s.Student),
                                                                                  a => a.Submissions.Select(s => s.SubmitFile));

            if (assignment == null)
            {
                return null;
            }

            AssignmentViewModel viewModel = new AssignmentViewModel
            {
                Assignment = assignment,
                Submissions = assignment.Submissions,
                SubmissionModel = new Submission(),
                StudentModel = new Student()
            };
            return viewModel;
        }

        public async Task<DetailsViewModel> GetSubmissionDetailsViewModelAsync(int assignmentId, string studentId)
        {
            Submission submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null)
            {
                return null;
            }
            DetailsViewModel viewModel = new DetailsViewModel
            {
                Submission = submission,
                AssignmentModel = new Assignment(),
                StudentModel = new Student()
            };
            return viewModel;
        }

        public async Task<EditViewModel> GetEditSubmissionViewModelAsync(int assignmentId, string studentId)
        {
            Submission submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null)
            {
                return null;
            }

            EditViewModel viewModel = new EditViewModel
            {
                StudentId = studentId,
                AssignmentId = assignmentId,
                Completed = submission.Completed,
                DueDate = submission.DueDate,
                Grade = (int)submission.Grade
            };
            return viewModel;
        }

        public async Task UpdateSubmissionAsync(EditViewModel viewModel)
        {
            Submission editedSubmission = new Submission
            {
                AssignmentId = viewModel.AssignmentId,
                StudentId = viewModel.StudentId,
                Grade = (byte?)viewModel.Grade,
                DueDate = viewModel.DueDate,
                Completed = viewModel.Completed
            };
            service.Update(editedSubmission,
                           s => s.DueDate,
                           s => s.Completed,
                           s => s.Grade);

            await service.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetDeleteSubmissionViewModelAsync(int assignmentId, string studentId)
        {
            Submission submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null)
            {
                return null;
            }
            DeleteViewModel viewModel = new DeleteViewModel
            {
                AssignmentModel = new Assignment(),
                StudentModel = new Student(),
                Submission = submission
            };
            return viewModel;
        }

        public async Task DeleteSubmissionAsync(int assignmentId, string studentId)
        {
            Submission submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission.SubmitFile != null)
            {
                DeleteFile(submission.SubmitFile);
            }
            service.Delete(submission);
            await service.SaveChangesAsync();
        }

        public async Task<IFileStreamWithInfo> GetSubmissionFileAsync(Controller controller, int assignmentId, string studentId)
        {
            Submission submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null || submission.SubmitFile == null)
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
            Submission submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null)
            {
                throw new Exception();
            }
            submission.Completed = submission.Completed == true ? false : true;
            await service.SaveChangesAsync();
            return submission.Completed;
        }

        public async Task<EvaluateViewModel> GetSubmissionEvaluateViewModelAsync(int assignmentId, string studentId)
        {
            Submission submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            if (submission == null)
            {
                return null;
            }
            EvaluateViewModel viewModel = new EvaluateViewModel
            {
                Submission = submission,
                Grade = (submission.Grade ?? 0),
                assignmentId = assignmentId,
                studentId = studentId
            };
            return viewModel;
        }

        public async Task EvaluateSubmissionAsync(EvaluateInputModel inputModel)
        {
            Submission submission = await service.GetByCompositeKeysAsync(inputModel.assignmentId, inputModel.studentId );
            if (submission == null)
            {
                throw new Exception();
            }

            submission.Grade = (byte)inputModel.Grade;
            await service.SaveChangesAsync();
        }

        public async Task UploadFileAsync(Controller controller, HttpPostedFileBase file, int assignmentId, string studentId)
        {
            Submission submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
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

                    service.DeleteFileFromFSandDBIfExists(submission.SubmitFile);

                    submission.SubmitFile = newSubmitFile;
                    submission.Submitted = DateTime.Now;

                    await service.SaveChangesAsync();
                }
                catch (Exception )
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

        private void DeleteFile(DataModel.Models.FileInfo file)
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
            IDisposable dispose = assigmentService as IDisposable;
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

        public async Task<Submission> GetSubmissionAsync(int assignmentId, string studentId)
        {
            Submission submission = await service.GetByCompositeKeysAsync(assignmentId, studentId );
            return submission;
        }
    }

    public class FileStreamWithInfo : IFileStreamWithInfo
    {
        public string FileName { get; set; }
        public byte[] FileStream { get; set; }
        public string FileType { get; set; }
    }

}