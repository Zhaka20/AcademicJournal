namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Completed = c.Boolean(nullable: false),
                        Grade = c.Byte(),
                        Created = c.DateTime(nullable: false),
                        Submitted = c.DateTime(),
                        DueDate = c.DateTime(),
                        TaskFileId = c.Int(),
                        SubmitFileId = c.Int(),
                        CreatorId = c.String(maxLength: 128),
                        StudentId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.TaskFiles", t => t.SubmitFileId)
                .ForeignKey("dbo.TaskFiles", t => t.TaskFileId)
                .Index(t => t.TaskFileId)
                .Index(t => t.SubmitFileId)
                .Index(t => t.CreatorId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 30),
                        LastName = c.String(maxLength: 30),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        MentorId = c.String(maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.MentorId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.MentorId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.AspNetUsers", t => t.Mentor_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id)
                .Index(t => t.Mentor_Id)
                .Index(t => t.Student_Id);
            
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
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Tests", t => t.Test_TestId)
                .Index(t => t.Test_TestId);
            
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        ChoiceId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Checked = c.Boolean(nullable: false),
                        Question_QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.ChoiceId)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId)
                .Index(t => t.Question_QuestionId);
            
            CreateTable(
                "dbo.TaskFiles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UploadFile = c.String(),
                        FileName = c.String(),
                        Assignment_AssignmentId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Assignments", t => t.Assignment_AssignmentId)
                .Index(t => t.Assignment_AssignmentId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Assignments", "TaskFileId", "dbo.TaskFiles");
            DropForeignKey("dbo.Assignments", "SubmitFileId", "dbo.TaskFiles");
            DropForeignKey("dbo.TaskFiles", "Assignment_AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.Tests", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "Test_TestId", "dbo.Tests");
            DropForeignKey("dbo.Choices", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Tests", "Mentor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "MentorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "CreatorId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TaskFiles", new[] { "Assignment_AssignmentId" });
            DropIndex("dbo.Choices", new[] { "Question_QuestionId" });
            DropIndex("dbo.Questions", new[] { "Test_TestId" });
            DropIndex("dbo.Tests", new[] { "Student_Id" });
            DropIndex("dbo.Tests", new[] { "Mentor_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "MentorId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Assignments", new[] { "StudentId" });
            DropIndex("dbo.Assignments", new[] { "CreatorId" });
            DropIndex("dbo.Assignments", new[] { "SubmitFileId" });
            DropIndex("dbo.Assignments", new[] { "TaskFileId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TaskFiles");
            DropTable("dbo.Choices");
            DropTable("dbo.Questions");
            DropTable("dbo.Tests");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Assignments");
        }
    }
}
