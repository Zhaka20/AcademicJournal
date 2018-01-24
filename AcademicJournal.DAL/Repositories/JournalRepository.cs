using AcademicJournal.DAL.Repositories.Common;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DAL.Context;
using System;

namespace AcademicJournal.DAL.Repositories
{
    public class JournalRepository : GenericRepository<Journal, int>, IJournalRepository,IDisposable
    {
        public JournalRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
