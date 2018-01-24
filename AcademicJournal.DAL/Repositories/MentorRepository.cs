using System;
using AcademicJournal.DAL.Context;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DAL.Repositories.Common;
using AcademicJournal.DALAbstraction.AbstractRepositories;

namespace AcademicJournal.DAL.Repositories
{
    public class MentorRepository : GenericRepository<Mentor, string>, IMentorRepository, IDisposable
    {
        public MentorRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
