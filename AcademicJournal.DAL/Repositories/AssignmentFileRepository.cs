using AcademicJournal.DAL.Repositories.Common;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DAL.Context;
using System;

namespace AcademicJournal.DAL.Repositories
{
    public class AssignmentFileRepository : GenericRepository<AssignmentFile, int>, IAssignmentFileRepository, IDisposable
    {
        public AssignmentFileRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}