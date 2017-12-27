namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskFileAssignmentIdforeingKeyadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskFiles", "Assignment_AssignmentId", "dbo.Assignments");
            DropIndex("dbo.TaskFiles", new[] { "Assignment_AssignmentId" });
            RenameColumn(table: "dbo.TaskFiles", name: "Assignment_AssignmentId", newName: "AssignmentId");
            AlterColumn("dbo.TaskFiles", "AssignmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.TaskFiles", "AssignmentId");
            AddForeignKey("dbo.TaskFiles", "AssignmentId", "dbo.Assignments", "AssignmentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskFiles", "AssignmentId", "dbo.Assignments");
            DropIndex("dbo.TaskFiles", new[] { "AssignmentId" });
            AlterColumn("dbo.TaskFiles", "AssignmentId", c => c.Int());
            RenameColumn(table: "dbo.TaskFiles", name: "AssignmentId", newName: "Assignment_AssignmentId");
            CreateIndex("dbo.TaskFiles", "Assignment_AssignmentId");
            AddForeignKey("dbo.TaskFiles", "Assignment_AssignmentId", "dbo.Assignments", "AssignmentId");
        }
    }
}
