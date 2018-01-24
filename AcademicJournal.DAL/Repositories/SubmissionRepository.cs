using AcademicJournal.DAL.Repositories.Common;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DAL.Context;
using System;

namespace AcademicJournal.DAL.Repositories
{
    public class SubmissionRepository : GenericRepository<Submission, object[]>, ISubmissionRepository, IDisposable
    {
        public SubmissionRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
