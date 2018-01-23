using AcademicJournal.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using AcademicJournal.DAL.Context;
using AcademicJournal.DataModel.Models;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class StudentService : IStudentService
    {
        ApplicationDbContext db;
        DbSet<Student> studentRepository;
        public StudentService(ApplicationDbContext db)
        {
            this.db = db;
            this.studentRepository = db.Set<Student>();
        }

        public void CreateStudent(Student student)
        {
            studentRepository.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            studentRepository.Remove(student);
        }

        public async Task DeleteStudentByIdAsync(string id)
        {
            ThrowIfNull(id);
            var student = await studentRepository.FindAsync(id);
            if (student != null)
            {
                DeleteStudent(student);
                return;
            }
            throw new ArgumentException("Student with given id does not exist!");
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await studentRepository.ToListAsync();
        }

        public async Task<Student> GetStudentByEmailAsync(string studentEmail)
        {
            ThrowIfNull(studentEmail);
            return await studentRepository.FirstOrDefaultAsync(s => s.Email == studentEmail);
        }

        public async Task<Student> GetStudentByIdAsync(string id)
        {
            ThrowIfNull(id);
            return await studentRepository.FindAsync(id);
        }

        public void InsertOrUpdateStudent(Student student)
        {
            ThrowIfNull(student);
            db.Entry(student).State = student.Id == null ?
                                    EntityState.Added :
                                    EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public void UpdateStudent(Student student)
        {
            ThrowIfNull(student);
            db.Students.Attach(student);
            student.UserName = student.Email;
            db.Entry(student).Property(e => e.Email).IsModified = true;
            db.Entry(student).Property(e => e.UserName).IsModified = true;
            db.Entry(student).Property(e => e.FirstName).IsModified = true;
            db.Entry(student).Property(e => e.LastName).IsModified = true;
            db.Entry(student).Property(e => e.PhoneNumber).IsModified = true;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        private void ThrowIfNull(object arg)
        {
            if (arg == null) throw new ArgumentNullException();
        }

    }
}
