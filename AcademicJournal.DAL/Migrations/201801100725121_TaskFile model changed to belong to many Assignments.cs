namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskFilemodelchangedtobelongtomanyAssignments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskFiles", "Assignment_AssignmentId", "dbo.Assignments");
            DropIndex("dbo.TaskFiles", new[] { "Assignment_AssignmentId" });
            AddColumn("dbo.Assignments", "TaskFile_id", c => c.Int());
            CreateIndex("dbo.Assignments", "TaskFile_id");
            AddForeignKey("dbo.Assignments", "TaskFile_id", "dbo.TaskFiles", "id");
            DropColumn("dbo.TaskFiles", "Assignment_AssignmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskFiles", "Assignment_AssignmentId", c => c.Int());
            DropForeignKey("dbo.Assignments", "TaskFile_id", "dbo.TaskFiles");
            DropIndex("dbo.Assignments", new[] { "TaskFile_id" });
            DropColumn("dbo.Assignments", "TaskFile_id");
            CreateIndex("dbo.TaskFiles", "Assignment_AssignmentId");
            AddForeignKey("dbo.TaskFiles", "Assignment_AssignmentId", "dbo.Assignments", "AssignmentId");
        }
    }
}
