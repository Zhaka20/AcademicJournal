using System;
using AcademicJournal.DAL.Context;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DAL.Repositories.Common;

namespace AcademicJournal.DAL.Repositories
{
    public class MentorRepository : GenericRepository<Mentor, string>, IDisposable
    {
        public MentorRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
