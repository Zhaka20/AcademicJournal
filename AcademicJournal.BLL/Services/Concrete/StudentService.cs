using AcademicJournal.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using AcademicJournal.DAL.Context;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DALAbstraction.AbstractRepositories;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class StudentService : IStudentService
    {
        private IStudentRepository studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public void CreateStudent(Student student)
        {
            studentRepository.Insert(student);
        }

        public void DeleteStudent(Student student)
        {
            studentRepository.Delete(student);
        }

        public async Task DeleteStudentByIdAsync(string id)
        {
            ThrowIfNull(id);
            var student = await studentRepository.GetSingleByIdAsync(id);
            if (student != null)
            {
                DeleteStudent(student);
                return;
            }
            throw new ArgumentException("Student with given id does not exist!");
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await studentRepository.GetAllAsync();
        }

        public async Task<Student> GetStudentByEmailAsync(string studentEmail)
        {
            ThrowIfNull(studentEmail);
            return await studentRepository.GetFirstOrDefaultAsync(s => s.Email == studentEmail);
        }

        public async Task<Student> GetStudentByIdAsync(string id)
        {
            ThrowIfNull(id);
            return await studentRepository.GetSingleByIdAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await studentRepository.SaveChangesAsync();
        }

        public void UpdateStudent(Student student)
        {
            ThrowIfNull(student);

            studentRepository.UpdateSelectedProperties(student, e => e.Email,
                                                                e => e.UserName,
                                                                e => e.FirstName,
                                                                e => e.LastName,
                                                                e => e.PhoneNumber);       
        }

        public void Dispose()
        {
            IDisposable disposable = studentRepository as IDisposable;
            if(disposable != null)
            {
                disposable.Dispose();
            }
        }

        private void ThrowIfNull(object arg)
        {
            if (arg == null) throw new ArgumentNullException();
        }

    }
}
