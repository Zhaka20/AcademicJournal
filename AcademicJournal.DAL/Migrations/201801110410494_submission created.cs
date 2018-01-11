namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class submissioncreated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "TaskFile_id", "dbo.TaskFiles");
            DropForeignKey("dbo.Assignments", "SubmitFileId", "dbo.TaskFiles");
            DropForeignKey("dbo.Assignments", "TaskFileId", "dbo.TaskFiles");
            DropIndex("dbo.Assignments", new[] { "TaskFileId" });
            DropIndex("dbo.Assignments", new[] { "SubmitFileId" });
            DropIndex("dbo.Assignments", new[] { "StudentId" });
            DropIndex("dbo.Assignments", new[] { "TaskFile_id" });
            CreateTable(
                "dbo.AssignmentFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileGuid = c.String(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Submissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Grade = c.Byte(),
                        Completed = c.Boolean(nullable: false),
                        DueDate = c.DateTime(),
                        Submitted = c.DateTime(),
                        StudentId = c.String(maxLength: 128),
                        AssignmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.AssignmentId);
            
            CreateTable(
                "dbo.SubmitFiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FileGuid = c.String(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Submissions", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.StudentAssignments",
                c => new
                    {
                        Student_Id = c.String(nullable: false, maxLength: 128),
                        Assignment_AssignmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Assignment_AssignmentId })
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id, cascadeDelete: true)
                .ForeignKey("dbo.Assignments", t => t.Assignment_AssignmentId, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => t.Assignment_AssignmentId);
            
            AddColumn("dbo.Assignments", "AssignmentFileId", c => c.Int());
            AddColumn("dbo.Comments", "Submission_Id", c => c.Int());
            CreateIndex("dbo.Assignments", "AssignmentFileId");
            CreateIndex("dbo.Comments", "Submission_Id");
            AddForeignKey("dbo.Assignments", "AssignmentFileId", "dbo.AssignmentFiles", "Id");
            AddForeignKey("dbo.Comments", "Submission_Id", "dbo.Submissions", "Id");
            DropColumn("dbo.Assignments", "Completed");
            DropColumn("dbo.Assignments", "Grade");
            DropColumn("dbo.Assignments", "Submitted");
            DropColumn("dbo.Assignments", "DueDate");
            DropColumn("dbo.Assignments", "TaskFileId");
            DropColumn("dbo.Assignments", "SubmitFileId");
            DropColumn("dbo.Assignments", "StudentId");
            DropColumn("dbo.Assignments", "TaskFile_id");
            DropTable("dbo.TaskFiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TaskFiles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UploadFile = c.String(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Assignments", "TaskFile_id", c => c.Int());
            AddColumn("dbo.Assignments", "StudentId", c => c.String(maxLength: 128));
            AddColumn("dbo.Assignments", "SubmitFileId", c => c.Int());
            AddColumn("dbo.Assignments", "TaskFileId", c => c.Int());
            AddColumn("dbo.Assignments", "DueDate", c => c.DateTime());
            AddColumn("dbo.Assignments", "Submitted", c => c.DateTime());
            AddColumn("dbo.Assignments", "Grade", c => c.Byte());
            AddColumn("dbo.Assignments", "Completed", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.SubmitFiles", "Id", "dbo.Submissions");
            DropForeignKey("dbo.Submissions", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Submission_Id", "dbo.Submissions");
            DropForeignKey("dbo.Submissions", "AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.StudentAssignments", "Assignment_AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.StudentAssignments", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "AssignmentFileId", "dbo.AssignmentFiles");
            DropIndex("dbo.StudentAssignments", new[] { "Assignment_AssignmentId" });
            DropIndex("dbo.StudentAssignments", new[] { "Student_Id" });
            DropIndex("dbo.SubmitFiles", new[] { "Id" });
            DropIndex("dbo.Submissions", new[] { "AssignmentId" });
            DropIndex("dbo.Submissions", new[] { "StudentId" });
            DropIndex("dbo.Comments", new[] { "Submission_Id" });
            DropIndex("dbo.Assignments", new[] { "AssignmentFileId" });
            DropColumn("dbo.Comments", "Submission_Id");
            DropColumn("dbo.Assignments", "AssignmentFileId");
            DropTable("dbo.StudentAssignments");
            DropTable("dbo.SubmitFiles");
            DropTable("dbo.Submissions");
            DropTable("dbo.AssignmentFiles");
            CreateIndex("dbo.Assignments", "TaskFile_id");
            CreateIndex("dbo.Assignments", "StudentId");
            CreateIndex("dbo.Assignments", "SubmitFileId");
            CreateIndex("dbo.Assignments", "TaskFileId");
            AddForeignKey("dbo.Assignments", "TaskFileId", "dbo.TaskFiles", "id");
            AddForeignKey("dbo.Assignments", "SubmitFileId", "dbo.TaskFiles", "id");
            AddForeignKey("dbo.Assignments", "TaskFile_id", "dbo.TaskFiles", "id");
            AddForeignKey("dbo.Assignments", "StudentId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
