using AcademicJournal.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using AcademicJournal.DAL.Models;
using AcademicJournal.DAL.Context;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class AssignmentService : IAssignmentService
    {
        private ApplicationDbContext db;
        public AssignmentService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Assignment>> GetAllAssignmentsAsync()
        {
            List<Assignment> assignments = await db.Assignments.
                                    Include(a => a.AssignmentFile).
                                    Include(a => a.Creator).
                                    Include(a => a.Submissions).
                                    ToListAsync();
            return assignments;
        }
        public async Task<Assignment> GetAssignmentByIdAsync(int id)
        {
            Assignment assignment = await db.Assignments.
                                          Include(a => a.AssignmentFile).
                                          Include(a => a.Creator).
                                          Include(a => a.Submissions).
                                          FirstOrDefaultAsync(s => s.AssignmentId == id);
            return assignment;
        }
        public void CreateAssignment(Assignment assignment)
        {
            db.Assignments.Add(assignment);
        }
        public void UpdateAssignment(Assignment assignment)
        {
            db.Assignments.Attach(assignment);
            db.Entry(assignment).Property(a => a.Title).IsModified = true;
        }
        public void DeleteAssignmentById(int id)
        {
            Assignment assignment = new Assignment
            {
                AssignmentId = id
            };
            db.Assignments.Attach(assignment);
            db.Assignments.Remove(assignment);         
        }
        public void DeleteAssignment(Assignment assignment)
        {
            if(db.Entry(assignment).State == EntityState.Detached)
            {
                db.Assignments.Attach(assignment);
            }
            db.Entry(assignment).State = EntityState.Deleted;         
        }
        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
