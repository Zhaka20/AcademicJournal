using AcademicJournal.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using AcademicJournal.DAL.Context;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class MentorService : GenericService<Mentor,string>
    {
        IGenericRepository<Student, string> studentsRepository;

        public MentorService(IGenericRepository<Mentor, string> repository, IGenericRepository<Student, string> studentRepository) : base(repository)
        {
            this.studentsRepository = studentRepository;
        }
              
        public async Task<Mentor> GetMentorByEmailAsync(string mentorEmail)
        {
            ThrowIfNull(mentorEmail);
            return await repository.GetFirstOrDefaultAsync(m => m.Email == mentorEmail);
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
    }
}
