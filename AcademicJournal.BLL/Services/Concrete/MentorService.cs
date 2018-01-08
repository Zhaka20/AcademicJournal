using AcademicJournal.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DAL.Models;
using System.Data.Entity;
using AcademicJournal.DAL.Context;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class MentorService : IMentorService
    {
        ApplicationDbContext db;
        DbSet<Mentor> mentorsRepositoy;
        public MentorService(ApplicationDbContext db)
        {
            this.db = db;
            this.mentorsRepositoy = db.Set<Mentor>();
        }

        public void CreateMentor(Mentor mentor)
        {
            ThrowIfNull(mentor);
            mentorsRepositoy.Add(mentor);
        }

        public void DeleteMentor(Mentor mentor)
        {
            ThrowIfNull(mentor);
            mentorsRepositoy.Remove(mentor);
        }

        public async Task DeleteMentorByIdAsync(string id)
        {
            ThrowIfNull(id);
            if (id == null) throw new ArgumentNullException("id cannot be null");
            var mentor = await mentorsRepositoy.FindAsync(id);
            if (mentor != null)
            {
                DeleteMentor(mentor);
                return;
            }
            throw new ArgumentException("Mentor with given id does not exist!");
        }

        public async Task<IEnumerable<Mentor>> GetAllMentorsAsync()
        {
            return await mentorsRepositoy.ToListAsync();
        }

        public async Task<Mentor> GetMentorByEmailAsync(string mentorEmail)
        {
            ThrowIfNull(mentorEmail);
            return await mentorsRepositoy.Where(m => m.Email == mentorEmail).FirstOrDefaultAsync();
        }

        public async Task<Mentor> GetMentorByIDAsync(string id)
        {
            ThrowIfNull(id);
            return await mentorsRepositoy.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public void UpdateMentor(Mentor mentor)
        {
            ThrowIfNull(mentor);
            db.Mentors.Attach(mentor);
            mentor.UserName = mentor.Email;
            db.Entry(mentor).Property(e => e.Email).IsModified = true;
            db.Entry(mentor).Property(e => e.UserName).IsModified = true;
            db.Entry(mentor).Property(e => e.FirstName).IsModified = true;
            db.Entry(mentor).Property(e => e.LastName).IsModified = true;
            db.Entry(mentor).Property(e => e.PhoneNumber).IsModified = true;
        }
       
        public async Task AcceptStudentAsync(string studentId, string mentorId)
        {
            ThrowIfNull(studentId,mentorId);

            var student = await db.Students.FindAsync(studentId);
            if(student == null)
            {
                throw new ArgumentException("Student with given id doesn't exit");
            }

            var mentor = await db.Mentors.FindAsync(mentorId);
            if (mentor == null)
            {
                throw new ArgumentException("Mentor with given id doesn't exit");
            }

            mentor.Students.Add(student);
        }

        public async Task RemoveStudentAsync(string studentId, string mentorId)
        {
            ThrowIfNull(studentId, mentorId);
             var student = await db.Students.FindAsync(studentId);
            if (student == null)
            {
                throw new ArgumentException("Student with given id doesn't exit");
            }

            var mentor = await db.Mentors.FindAsync(mentorId);
            if (mentor == null)
            {
                throw new ArgumentException("Mentor with given id doesn't exit");
            }

            mentor.Students.Remove(student);
        }

        public void InsertOrUpdateMentor(Mentor mentor)
        {
            ThrowIfNull(mentor);
            db.Entry(mentor).State = mentor.Id == null ?
                                     EntityState.Added :
                                     EntityState.Modified;
        }

        public void Dispose()
        {
            db.Dispose();
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
