using System;
using AcademicJournal.DAL.Context;
using AcademicJournal.DataModel.Models;

namespace AcademicJournal.DAL.Repositories
{
    public class StudentRepository : GenericRepository<Student, string>, IDisposable
    {
        public StudentRepository(ApplicationDbContext db) : base(db)
        {
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
