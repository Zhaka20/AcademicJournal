﻿using System.Data.Entity;
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
            modelBuilder.Entity<Assignment>().
                HasOptional(a => a.Creator).
                WithOptionalDependent();

            modelBuilder.Entity<Student>().
                HasMany(s => s.Assignments).
                WithOptional(a => a.Student).
                WillCascadeOnDelete(true);

            modelBuilder.Entity<Mentor>().
               HasMany(s => s.Assignments).
               WithOptional(a => a.Creator).
               HasForeignKey(a => a.CreatorId).
               WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}