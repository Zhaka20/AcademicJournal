namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somechanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubmitFiles", new[] { "Submission_AssignmentId", "Submission_StudentId" }, "dbo.Submissions");
            AddForeignKey("dbo.SubmitFiles", new[] { "Submission_AssignmentId", "Submission_StudentId" }, "dbo.Submissions", new[] { "AssignmentId", "StudentId" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubmitFiles", new[] { "Submission_AssignmentId", "Submission_StudentId" }, "dbo.Submissions");
            AddForeignKey("dbo.SubmitFiles", new[] { "Submission_AssignmentId", "Submission_StudentId" }, "dbo.Submissions", new[] { "AssignmentId", "StudentId" });
        }
    }
}
