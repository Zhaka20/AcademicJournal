using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;
using AcademicJournal.BLL.Services.Abstract;
using System.IO;
using AcademicJournal.DALAbstraction.AbstractRepositories;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class SubmissionService : GenericService<Submission, object[]>, ISubmissionService
    {
        protected readonly ISubmitFileService submitFileService;

        public SubmissionService(ISubmissionRepository repository, ISubmitFileService submitFileService) : base(repository)
        {
            this.submitFileService = submitFileService;
        }

        public void DeleteFileFromFSandDBIfExists(SubmitFile submitFile)
        {
            if (submitFile != null)
            {
                DeleteFile(submitFile);
            }
            submitFileService.Delete(submitFile);
        }

        protected void DeleteFile(DataModel.Models.FileInfo file)
        {
            if (file == null) return;

            string fullPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Files/Assignments"), file.FileGuid);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        public override void Dispose()
        {
            IDisposable dispose = submitFileService as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
            base.Dispose();
        }
    }
}
