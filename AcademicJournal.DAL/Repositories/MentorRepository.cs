using System;
using AcademicJournal.DAL.Context;
using AcademicJournal.DataModel.Models;

namespace AcademicJournal.DAL.Repositories
{
    public class MentorRepository : GenericRepository<Mentor, string>, IDisposable
    {
        public MentorRepository(ApplicationDbContext db) : base(db)
        {
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
