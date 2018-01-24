using AcademicJournal.DAL.Repositories.Common;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DAL.Context;
using AcademicJournal.DALAbstraction.AbstractRepositories;

namespace AcademicJournal.DAL.Repositories
{
    public class CommentRepository : GenericRepository<Comment, int>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
