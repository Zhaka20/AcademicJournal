namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class submissionmodeladdedtestmodeldeletedassignmentmodelchangedassignmentfileandsubmitfilemodeladded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.Assignments", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tests", "Mentor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Choices", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "Test_TestId", "dbo.Tests");
            DropForeignKey("dbo.Tests", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "TaskFile_id", "dbo.TaskFiles");
            DropForeignKey("dbo.Assignments", "SubmitFileId", "dbo.TaskFiles");
            DropForeignKey("dbo.Assignments", "TaskFileId", "dbo.TaskFiles");
            DropForeignKey("dbo.StudentAssignments", "Assignment_Id", "dbo.Assignments");
            DropForeignKey("dbo.Submissions", "AssignmentId", "dbo.Assignments");
            DropIndex("dbo.Assignments", new[] { "TaskFileId" });
            DropIndex("dbo.Assignments", new[] { "SubmitFileId" });
            DropIndex("dbo.Assignments", new[] { "StudentId" });
            DropIndex("dbo.Assignments", new[] { "TaskFile_id" });
            DropIndex("dbo.Comments", new[] { "AssignmentId" });
            DropIndex("dbo.Tests", new[] { "Mentor_Id" });
            DropIndex("dbo.Tests", new[] { "Student_Id" });
            DropIndex("dbo.Questions", new[] { "Test_TestId" });
            DropIndex("dbo.Choices", new[] { "Question_QuestionId" });
            DropPrimaryKey("dbo.Assignments");
            DropColumn("dbo.Assignments", "AssignmentId");
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
                .ForeignKey("dbo.Submissions", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.StudentAssignments",
                c => new
                    {
                        Student_Id = c.String(nullable: false, maxLength: 128),
                        Assignment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Assignment_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id, cascadeDelete: true)
                .ForeignKey("dbo.Assignments", t => t.Assignment_Id, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => t.Assignment_Id);
            
            AddColumn("dbo.Assignments", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Assignments", "AssignmentFileId", c => c.Int());
            AddColumn("dbo.Comments", "SubmissionId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Assignments", "Id");
            CreateIndex("dbo.Assignments", "AssignmentFileId");
            CreateIndex("dbo.Comments", "SubmissionId");
            AddForeignKey("dbo.Assignments", "AssignmentFileId", "dbo.AssignmentFiles", "Id");
            AddForeignKey("dbo.Comments", "SubmissionId", "dbo.Submissions", "Id", cascadeDelete: true);
            DropColumn("dbo.Assignments", "Completed");
            DropColumn("dbo.Assignments", "Grade");
            DropColumn("dbo.Assignments", "Submitted");
            DropColumn("dbo.Assignments", "DueDate");
            DropColumn("dbo.Assignments", "TaskFileId");
            DropColumn("dbo.Assignments", "SubmitFileId");
            DropColumn("dbo.Assignments", "StudentId");
            DropColumn("dbo.Assignments", "TaskFile_id");
            DropColumn("dbo.Comments", "AssignmentId");
            DropTable("dbo.Tests");
            DropTable("dbo.Questions");
            DropTable("dbo.Choices");
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
            
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        ChoiceId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Checked = c.Boolean(nullable: false),
                        Question_QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.ChoiceId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Text = c.String(),
                        MyProperty = c.Int(nullable: false),
                        Test_TestId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionId);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        FinishTime = c.DateTime(nullable: false),
                        TotalScore = c.Int(nullable: false),
                        Mentor_Id = c.String(maxLength: 128),
                        Student_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TestId);
            
            AddColumn("dbo.Comments", "AssignmentId", c => c.Int(nullable: false));
            AddColumn("dbo.Assignments", "TaskFile_id", c => c.Int());
            AddColumn("dbo.Assignments", "StudentId", c => c.String(maxLength: 128));
            AddColumn("dbo.Assignments", "SubmitFileId", c => c.Int());
            AddColumn("dbo.Assignments", "TaskFileId", c => c.Int());
            AddColumn("dbo.Assignments", "DueDate", c => c.DateTime());
            AddColumn("dbo.Assignments", "Submitted", c => c.DateTime());
            AddColumn("dbo.Assignments", "Grade", c => c.Byte());
            AddColumn("dbo.Assignments", "Completed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Assignments", "AssignmentId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.SubmitFiles", "Id", "dbo.Submissions");
            DropForeignKey("dbo.Submissions", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "SubmissionId", "dbo.Submissions");
            DropForeignKey("dbo.Submissions", "AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.StudentAssignments", "Assignment_Id", "dbo.Assignments");
            DropForeignKey("dbo.StudentAssignments", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "AssignmentFileId", "dbo.AssignmentFiles");
            DropIndex("dbo.StudentAssignments", new[] { "Assignment_Id" });
            DropIndex("dbo.StudentAssignments", new[] { "Student_Id" });
            DropIndex("dbo.SubmitFiles", new[] { "Id" });
            DropIndex("dbo.Submissions", new[] { "AssignmentId" });
            DropIndex("dbo.Submissions", new[] { "StudentId" });
            DropIndex("dbo.Comments", new[] { "SubmissionId" });
            DropIndex("dbo.Assignments", new[] { "AssignmentFileId" });
            DropPrimaryKey("dbo.Assignments");
            DropColumn("dbo.Comments", "SubmissionId");
            DropColumn("dbo.Assignments", "AssignmentFileId");
            DropColumn("dbo.Assignments", "Id");
            DropTable("dbo.StudentAssignments");
            DropTable("dbo.SubmitFiles");
            DropTable("dbo.Submissions");
            DropTable("dbo.AssignmentFiles");
            AddPrimaryKey("dbo.Assignments", "AssignmentId");
            CreateIndex("dbo.Choices", "Question_QuestionId");
            CreateIndex("dbo.Questions", "Test_TestId");
            CreateIndex("dbo.Tests", "Student_Id");
            CreateIndex("dbo.Tests", "Mentor_Id");
            CreateIndex("dbo.Comments", "AssignmentId");
            CreateIndex("dbo.Assignments", "TaskFile_id");
            CreateIndex("dbo.Assignments", "StudentId");
            CreateIndex("dbo.Assignments", "SubmitFileId");
            CreateIndex("dbo.Assignments", "TaskFileId");
            AddForeignKey("dbo.Submissions", "AssignmentId", "dbo.Assignments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StudentAssignments", "Assignment_Id", "dbo.Assignments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Assignments", "TaskFileId", "dbo.TaskFiles", "id");
            AddForeignKey("dbo.Assignments", "SubmitFileId", "dbo.TaskFiles", "id");
            AddForeignKey("dbo.Assignments", "TaskFile_id", "dbo.TaskFiles", "id");
            AddForeignKey("dbo.Tests", "Student_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Questions", "Test_TestId", "dbo.Tests", "TestId");
            AddForeignKey("dbo.Choices", "Question_QuestionId", "dbo.Questions", "QuestionId");
            AddForeignKey("dbo.Tests", "Mentor_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Assignments", "StudentId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "AssignmentId", "dbo.Assignments", "AssignmentId", cascadeDelete: true);
        }
    }
}
