namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToOneTaskFilePK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "CreatorId", "dbo.AspNetUsers");
            AddColumn("dbo.TaskFiles", "Assignment_AssignmentId", c => c.Int());
            CreateIndex("dbo.TaskFiles", "Assignment_AssignmentId");
            AddForeignKey("dbo.TaskFiles", "Assignment_AssignmentId", "dbo.Assignments", "AssignmentId");
            AddForeignKey("dbo.Assignments", "CreatorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TaskFiles", "Assignment_AssignmentId", "dbo.Assignments");
            DropIndex("dbo.TaskFiles", new[] { "Assignment_AssignmentId" });
            DropColumn("dbo.TaskFiles", "Assignment_AssignmentId");
            AddForeignKey("dbo.Assignments", "CreatorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
