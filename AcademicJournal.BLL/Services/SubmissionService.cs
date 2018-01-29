using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DataModel.Models;
using System;
using AcademicJournal.AbstractBLL.AbstractServices;
using System.IO;
using AcademicJournal.FrontToBLL_DTOs.DTOs;
using AcademicJournal.BLL.Services.Common;
using AcademicJournal.BLL.Services.Common.Interfaces;
using AcademicJournal.Services.Common;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class SubmissionService : BasicCRUDService<SubmissionDTO, object[]>, ISubmissionService,IDisposable
    {
        protected readonly ISubmitFileService submitFileService;
        protected readonly GenericService<Submission, object[]> service;
        protected readonly IObjectMapper mapper;

        public SubmissionService(GenericService<Submission, object[]> service,
                                 ISubmitFileService submitFileService, 
                                 IGenericDTOService<SubmissionDTO,object[]> dtoService,
                                 IObjectMapper mapper)
                                : base(dtoService)
        {
            this.service = service;
            this.submitFileService = submitFileService;
            this.mapper = mapper;

        }

        public void DeleteFileFromFSandDBIfExists(SubmitFileDTO dto)
        {
            if(dto == null)
            {
                return;
            }
            var submitFileInfo = mapper.Map<SubmitFileDTO, SubmitFile>(dto);
            if(submitFileInfo == null)
            {
                throw new ArgumentException("Could not convert the argument to a proper entity type.");
            }
            if (submitFileInfo != null)
            {
                DeleteFile(submitFileInfo);
            }
            submitFileService.Delete(dto);
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

        public void Dispose()
        {
            IDisposable dispose = submitFileService as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }

            dispose = dtoService as IDisposable;
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

    
    }
}
