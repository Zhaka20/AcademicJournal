using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using AcademicJournal.DAL.Models;

namespace AcademicJournal.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("AcademicJournalContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Mentor> Mentors { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<SubmitFile> SubmitFiles { get; set; }
        public virtual DbSet<AssignmentFile> AssignmentFiles { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<WorkDay> WorkDays { get; set; }
        public virtual DbSet<Journal> Journals { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>().
                HasOptional(a => a.Creator).
                WithMany(c => c.Assignments);

            modelBuilder.Entity<Submission>().
               HasOptional(s => s.SubmitFile).
               WithRequired(s => s.Submission);

            modelBuilder.Entity<Student>().
                HasMany(s => s.Assignments).
                WithMany(a => a.Students);

            modelBuilder.Entity<Mentor>().
               HasMany(s => s.Assignments).
               WithOptional(a => a.Creator).
               HasForeignKey(a => a.CreatorId).
               WillCascadeOnDelete(false);

            modelBuilder.Entity<Mentor>().
                HasMany(a => a.Journals).
                WithOptional( j => j.Mentor);

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<AcademicJournal.DAL.Models.Submission> Submissions { get; set; }
    }
}