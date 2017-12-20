﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DAL.Models;
using AcademicJournal.BLL.Repository.Abstract;
using System.Linq.Expressions;
using AcademicJournal.DAL.Context;
using System.Data.Entity;

namespace AcademicJournal.BLL.Repository.Concrete
{
    public class StudentRepository : IStudentRepository
    {
        ApplicationDbContext context;
        DbSet<Student> dbSet;
        private bool disposed = false;
        public StudentRepository(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
            dbSet = context.Set<Student>();
        }

        public void Add(Student student)
        {
            dbSet.Add(student);
        }

        public void Delete(Student student)
        {
            if (context.Entry(student).State == EntityState.Detached)
            {
                dbSet.Attach(student);
            }
            dbSet.Remove(student);
        }

        public async Task Delete(string id)
        {
            Student entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public IQueryable<Student> Query(Expression<Func<Student, bool>> filter = null, Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Student> query = dbSet;

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

        public void Update(Student student)
        {
            dbSet.Attach(student);
            context.Entry(student).State = EntityState.Modified;
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
