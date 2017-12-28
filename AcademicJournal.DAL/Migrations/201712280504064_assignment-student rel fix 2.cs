namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assignmentstudentrelfix2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "Student_Id1", "dbo.AspNetUsers");
            AddForeignKey("dbo.Assignments", "Student_Id1", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "Student_Id1", "dbo.AspNetUsers");
            AddForeignKey("dbo.Assignments", "Student_Id1", "dbo.AspNetUsers", "Id");
        }
    }
}
