namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assignmentstudentrelfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "StudentId", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "StudentId" });
            AddColumn("dbo.Assignments", "Student_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Assignments", "Student_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Assignments", "StudentId", c => c.String());
            CreateIndex("dbo.Assignments", "Student_Id");
            CreateIndex("dbo.Assignments", "Student_Id1");
            AddForeignKey("dbo.Assignments", "Student_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Assignments", "Student_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "Student_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "Student_Id1" });
            DropIndex("dbo.Assignments", new[] { "Student_Id" });
            AlterColumn("dbo.Assignments", "StudentId", c => c.String(maxLength: 128));
            DropColumn("dbo.Assignments", "Student_Id1");
            DropColumn("dbo.Assignments", "Student_Id");
            CreateIndex("dbo.Assignments", "StudentId");
            AddForeignKey("dbo.Assignments", "StudentId", "dbo.AspNetUsers", "Id");
        }
    }
}
