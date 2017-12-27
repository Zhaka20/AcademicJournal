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
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<TaskFile> TaskFiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Mentor>().HasMany(a => a.Assignments).
                   WithOptional(a => a.Creator).WillCascadeOnDelete();
        }
    }
}