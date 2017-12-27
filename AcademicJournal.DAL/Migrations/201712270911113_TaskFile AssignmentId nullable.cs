namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskFileAssignmentIdnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskFiles", "AssignmentId", "dbo.Assignments");
            DropIndex("dbo.TaskFiles", new[] { "AssignmentId" });
            AlterColumn("dbo.TaskFiles", "AssignmentId", c => c.Int());
            CreateIndex("dbo.TaskFiles", "AssignmentId");
            AddForeignKey("dbo.TaskFiles", "AssignmentId", "dbo.Assignments", "AssignmentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskFiles", "AssignmentId", "dbo.Assignments");
            DropIndex("dbo.TaskFiles", new[] { "AssignmentId" });
            AlterColumn("dbo.TaskFiles", "AssignmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.TaskFiles", "AssignmentId");
            AddForeignKey("dbo.TaskFiles", "AssignmentId", "dbo.Assignments", "AssignmentId", cascadeDelete: true);
        }
    }
}
