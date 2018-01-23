using System;
using AcademicJournal.DAL.Context;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.DAL.Repositories.Common;

namespace AcademicJournal.DAL.Repositories
{
    public class StudentRepository : GenericRepository<Student, string>, IDisposable
    {
        public StudentRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
