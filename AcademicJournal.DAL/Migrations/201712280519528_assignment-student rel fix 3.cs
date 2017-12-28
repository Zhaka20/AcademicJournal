namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assignmentstudentrelfix3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "Student_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "Student_Id" });
            DropIndex("dbo.Assignments", new[] { "Student_Id1" });
            DropColumn("dbo.Assignments", "StudentId");
            DropColumn("dbo.Assignments", "StudentId");
            RenameColumn(table: "dbo.Assignments", name: "Student_Id1", newName: "StudentId");
            RenameColumn(table: "dbo.Assignments", name: "Student_Id", newName: "StudentId");
            AlterColumn("dbo.Assignments", "StudentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Assignments", "StudentId");
            AddForeignKey("dbo.Assignments", "StudentId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "StudentId", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "StudentId" });
            AlterColumn("dbo.Assignments", "StudentId", c => c.String());
            RenameColumn(table: "dbo.Assignments", name: "StudentId", newName: "Student_Id");
            RenameColumn(table: "dbo.Assignments", name: "StudentId", newName: "Student_Id1");
            AddColumn("dbo.Assignments", "StudentId", c => c.String());
            AddColumn("dbo.Assignments", "StudentId", c => c.String());
            CreateIndex("dbo.Assignments", "Student_Id1");
            CreateIndex("dbo.Assignments", "Student_Id");
            AddForeignKey("dbo.Assignments", "Student_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
