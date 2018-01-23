using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Services.Abstract
{
    public interface IStudentService : IDisposable
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByEmailAsync(string studentEmail);
        Task<Student> GetStudentByIdAsync(string id);
        void UpdateStudent(Student student);
        void InsertOrUpdateStudent(Student student);
        void CreateStudent(Student student);
        void DeleteStudent(Student student);
        Task DeleteStudentByIdAsync(string id);
        Task SaveChangesAsync();
    }
}
