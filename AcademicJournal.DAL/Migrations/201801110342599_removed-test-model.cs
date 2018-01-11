namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedtestmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tests", "Mentor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Choices", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "Test_TestId", "dbo.Tests");
            DropForeignKey("dbo.Tests", "Student_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tests", new[] { "Mentor_Id" });
            DropIndex("dbo.Tests", new[] { "Student_Id" });
            DropIndex("dbo.Questions", new[] { "Test_TestId" });
            DropIndex("dbo.Choices", new[] { "Question_QuestionId" });
            DropTable("dbo.Tests");
            DropTable("dbo.Questions");
            DropTable("dbo.Choices");
        }
        
        public override void Down()
        {
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
            
            CreateIndex("dbo.Choices", "Question_QuestionId");
            CreateIndex("dbo.Questions", "Test_TestId");
            CreateIndex("dbo.Tests", "Student_Id");
            CreateIndex("dbo.Tests", "Mentor_Id");
            AddForeignKey("dbo.Tests", "Student_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Questions", "Test_TestId", "dbo.Tests", "TestId");
            AddForeignKey("dbo.Choices", "Question_QuestionId", "dbo.Questions", "QuestionId");
            AddForeignKey("dbo.Tests", "Mentor_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
