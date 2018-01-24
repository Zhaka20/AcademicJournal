using AcademicJournal.DAL.Repositories.Common;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.DataModel.Models;
using System;
using AcademicJournal.DAL.Context;

namespace AcademicJournal.DAL.Repositories
{
    public class SubmitFileRepository : GenericRepository<SubmitFile, int>, ISubmitFileRepository, IDisposable
    {
        public SubmitFileRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}