using AcademicJournal.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DAL.Models;
using AcademicJournal.BLL.Repository.Abstract;
using System.Data.Entity;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class StudentService : IStudentService
    {
        IStudentRepository studentRepository;
        public StudentService(IStudentRepository repo)
        {
            this.studentRepository = repo;
        }

        public void CreateStudent(Student student)
        {
            studentRepository.Add(student);
        }

        public void DeleteStudent(string id)
        {
            studentRepository.Delete(id);
        }
    
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await studentRepository.Query().ToListAsync();
        }

        public async Task<Student> GetStudentByEmailAsync(string studentEmail)
        {
           return await studentRepository.Query().FirstOrDefaultAsync(s => s.Email == studentEmail);
        }

        public async Task<Student> GetStudentByIDAsync(string id)
        {
            return await studentRepository.Query().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await studentRepository.SaveChangesAsync();
        }

        public void UpdateStudent(Student student)
        {
            studentRepository.Update(student);
        }

        public void Dispose()
        {
            studentRepository.Dispose();
        }
    }
}
