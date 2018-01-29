using AcademicJournal.AbstractBLL.AbstractServices;
using System;
using System.Threading.Tasks;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.BLL.Services.Common;
using AcademicJournal.FrontToBLL_DTOs.DTOs;
using AcademicJournal.BLL.Services.Common.Interfaces;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class MentorService : BasicCRUDService<MentorDTO,string>, IMentorService, IDisposable
    {
        protected readonly IGenericService<Mentor, string> service;
        public MentorService(IGenericService<Mentor, string> service, IGenericDTOService<MentorDTO, string> dtoService) : base(dtoService)
        {
            this.service = service;
        }
     
        public async Task<Mentor> GetMentorByEmailAsync(string mentorEmail)
        {
            ThrowIfNull(mentorEmail);
            return await service.GetFirstOrDefaultAsync(m => m.Email == mentorEmail);
        }
       
        public async Task AcceptStudentAsync(string studentId, string mentorId)
        {
            ThrowIfNull(studentId,mentorId);

            var student = await studentsRepository.GetSingleByIdAsync(studentId);
            if(student == null)
            {
                throw new ArgumentException("Student with given id doesn't exit");
            }

            var mentor = await repository.GetSingleByIdAsync(mentorId);
            if (mentor == null)
            {
                throw new ArgumentException("Mentor with given id doesn't exit");
            }

            mentor.Students.Add(student);
        }

        public async Task RemoveStudentAsync(string studentId, string mentorId)
        {
            ThrowIfNull(studentId, mentorId);
             var student = await studentsRepository.GetSingleByIdAsync(studentId);
            if (student == null)
            {
                throw new ArgumentException("Student with given id doesn't exit");
            }

            var mentor = await repository.GetSingleByIdAsync(mentorId);
            if (mentor == null)
            {
                throw new ArgumentException("Mentor with given id doesn't exit");
            }

            mentor.Students.Remove(student);
        }
 
        private void ThrowIfNull(params object[] args)
        {
            foreach (var arg in args)
            {
                if (arg == null) throw new ArgumentNullException();
            }
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

        Task<MentorDTO> IMentorService.GetMentorByEmailAsync(string mentorEmail)
        {
            throw new NotImplementedException();
        }
    }
}
