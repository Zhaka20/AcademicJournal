using AcademicJournal.AbstractBLL.AbstractServices;
using System;
using System.Threading.Tasks;
using AcademicJournal.DataModel.Models;
using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.BLL.Services.Common;
using AcademicJournal.BLL.Services.Common.Interfaces;
using AcademicJournal.FrontToBLL_DTOs.DTOs;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class StudentService : BasicCRUDService<StudentDTO, string>, IStudentService, IDisposable
    {
        protected readonly GenericService<Student, string> service;
        public StudentService(GenericService<Student, string> service, IGenericDTOService<StudentDTO, string> dtoService) : base(dtoService)
        {
            this.service = service;
        }

        async Task<StudentDTO> IStudentService.GetStudentByEmailAsync(string studentEmail)
        {
            ThrowIfNull(studentEmail);
            return await repository.GetFirstOrDefaultAsync(s => s.Email == studentEmail);
        }

        private void ThrowIfNull(object arg)
        {
            if (arg == null) throw new ArgumentNullException();
        }

        public void Dispose()
        {
            IDisposable dispose = dtoService as IDisposable;
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
