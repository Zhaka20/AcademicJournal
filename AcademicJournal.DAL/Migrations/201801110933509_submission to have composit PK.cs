namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class submissiontohavecompositPK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Submissions", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Submission_Id", "dbo.Submissions");
            DropForeignKey("dbo.SubmitFiles", "Id", "dbo.Submissions");
            DropForeignKey("dbo.Comments", new[] { "Submission_AssignmentId", "Submission_StudentId" }, "dbo.Submissions");
            DropForeignKey("dbo.SubmitFiles", new[] { "Submission_AssignmentId", "Submission_StudentId" }, "dbo.Submissions");
            DropIndex("dbo.Comments", new[] { "Submission_Id" });
            DropIndex("dbo.Submissions", new[] { "StudentId" });
            DropIndex("dbo.SubmitFiles", new[] { "Id" });
            RenameColumn(table: "dbo.Comments", name: "Submission_Id", newName: "Submission_AssignmentId");
            DropPrimaryKey("dbo.Submissions");
            DropPrimaryKey("dbo.SubmitFiles");
            AddColumn("dbo.Comments", "Submission_StudentId", c => c.String(maxLength: 128));
            AddColumn("dbo.SubmitFiles", "Submission_AssignmentId", c => c.Int(nullable: false));
            AddColumn("dbo.SubmitFiles", "Submission_StudentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Submissions", "StudentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.SubmitFiles", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Submissions", new[] { "AssignmentId", "StudentId" });
            AddPrimaryKey("dbo.SubmitFiles", "Id");
            CreateIndex("dbo.Comments", new[] { "Submission_AssignmentId", "Submission_StudentId" });
            CreateIndex("dbo.Submissions", "StudentId");
            CreateIndex("dbo.SubmitFiles", new[] { "Submission_AssignmentId", "Submission_StudentId" });
            AddForeignKey("dbo.Submissions", "StudentId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", new[] { "Submission_AssignmentId", "Submission_StudentId" }, "dbo.Submissions", new[] { "AssignmentId", "StudentId" });
            AddForeignKey("dbo.SubmitFiles", new[] { "Submission_AssignmentId", "Submission_StudentId" }, "dbo.Submissions", new[] { "AssignmentId", "StudentId" });
            DropColumn("dbo.Submissions", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Submissions", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.SubmitFiles", new[] { "Submission_AssignmentId", "Submission_StudentId" }, "dbo.Submissions");
            DropForeignKey("dbo.Comments", new[] { "Submission_AssignmentId", "Submission_StudentId" }, "dbo.Submissions");
            DropForeignKey("dbo.Submissions", "StudentId", "dbo.AspNetUsers");
            DropIndex("dbo.SubmitFiles", new[] { "Submission_AssignmentId", "Submission_StudentId" });
            DropIndex("dbo.Submissions", new[] { "StudentId" });
            DropIndex("dbo.Comments", new[] { "Submission_AssignmentId", "Submission_StudentId" });
            DropPrimaryKey("dbo.SubmitFiles");
            DropPrimaryKey("dbo.Submissions");
            AlterColumn("dbo.SubmitFiles", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Submissions", "StudentId", c => c.String(maxLength: 128));
            DropColumn("dbo.SubmitFiles", "Submission_StudentId");
            DropColumn("dbo.SubmitFiles", "Submission_AssignmentId");
            DropColumn("dbo.Comments", "Submission_StudentId");
            AddPrimaryKey("dbo.SubmitFiles", "Id");
            AddPrimaryKey("dbo.Submissions", "Id");
            RenameColumn(table: "dbo.Comments", name: "Submission_AssignmentId", newName: "Submission_Id");
            CreateIndex("dbo.SubmitFiles", "Id");
            CreateIndex("dbo.Submissions", "StudentId");
            CreateIndex("dbo.Comments", "Submission_Id");
            AddForeignKey("dbo.SubmitFiles", new[] { "Submission_AssignmentId", "Submission_StudentId" }, "dbo.Submissions", new[] { "AssignmentId", "StudentId" });
            AddForeignKey("dbo.Comments", new[] { "Submission_AssignmentId", "Submission_StudentId" }, "dbo.Submissions", new[] { "AssignmentId", "StudentId" });
            AddForeignKey("dbo.SubmitFiles", "Id", "dbo.Submissions", "Id");
            AddForeignKey("dbo.Comments", "Submission_Id", "dbo.Submissions", "Id");
            AddForeignKey("dbo.Submissions", "StudentId", "dbo.AspNetUsers", "Id");
        }
    }
}
