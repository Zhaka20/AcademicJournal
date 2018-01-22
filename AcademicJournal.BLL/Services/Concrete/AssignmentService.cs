using AcademicJournal.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using AcademicJournal.DAL.Models;
using AcademicJournal.DAL.Context;
using System.Web;
using System.IO;

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
        public void DeleteAssignment(Assignment assignment)
        {
            db.Assignments.Remove(assignment);
        }
        public async Task DeleteAssignmentByIdWithSubmissionAndAssignmentFilesAsync(int id)
        {
            Assignment assignment = await db.Assignments.Include(a => a.AssignmentFile).
                                                         Include(a => a.Submissions.Select(s => s.SubmitFile)).
                                                         FirstOrDefaultAsync(a => a.AssignmentId == id);

            foreach (Submission submission in assignment.Submissions)
            {
                DeleteFile(submission.SubmitFile);
            }
            DeleteFile(assignment.AssignmentFile);

            db.Assignments.Remove(assignment);
            await db.SaveChangesAsync();
        }
        public void UpdateAssignment(Assignment assignment)
        {
            db.Assignments.Attach(assignment);
            db.Entry(assignment).Property(a => a.Title).IsModified = true;
        }
        public void InsertOrUpdateAssignment(Assignment assignment)
        {
            bool state = db.Entry(assignment).State == EntityState.Detached;
            if (state)
            {
                db.Assignments.Add(assignment);
            }
            else
            {
                db.Entry(assignment).State = EntityState.Modified;
            }
        }
        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }
        public void Dispose()
        {
            db.Dispose();
        }
        private void DeleteFile(DAL.Models.FileInfo file)
        {
            if (file == null) return;

            string fullPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Files/Assignments"), file.FileGuid);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}
