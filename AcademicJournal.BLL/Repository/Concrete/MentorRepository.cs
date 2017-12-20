using AcademicJournal.BLL.Repository.Abstract;
using AcademicJournal.DAL.Context;
using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Repository.Concrete
{
    public class MentorRepository : IMentorRepository
    {
        ApplicationDbContext context;
        DbSet<Mentor> dbSet;
        private bool disposed = false;
        public MentorRepository(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
            dbSet = context.Set<Mentor>();
        }

        public void Add(Mentor mentor)
        {
            dbSet.Add(mentor);
        }

        public void Delete(Mentor mentor)
        {
            if (context.Entry(mentor).State == EntityState.Detached)
            {
                dbSet.Attach(mentor);
            }
            dbSet.Remove(mentor);
        }

        public async Task Delete(string id)
        {
            Mentor entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public IQueryable<Mentor> Query(Expression<Func<Mentor, bool>> filter = null, Func<IQueryable<Mentor>, IOrderedQueryable<Mentor>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Mentor> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update(Mentor mentor)
        {
            dbSet.Attach(mentor);
            context.Entry(mentor).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
