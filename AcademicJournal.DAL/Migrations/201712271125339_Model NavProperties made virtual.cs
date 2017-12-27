namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelNavPropertiesmadevirtual : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskFiles", "AssignmentId", "dbo.Assignments");
            DropIndex("dbo.TaskFiles", new[] { "AssignmentId" });
            RenameColumn(table: "dbo.Assignments", name: "SubmitFile_id", newName: "SubmitFileId");
            RenameColumn(table: "dbo.Assignments", name: "TaskFile_id", newName: "TaskFileId");
            RenameIndex(table: "dbo.Assignments", name: "IX_TaskFile_id", newName: "IX_TaskFileId");
            RenameIndex(table: "dbo.Assignments", name: "IX_SubmitFile_id", newName: "IX_SubmitFileId");
            DropColumn("dbo.TaskFiles", "AssignmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskFiles", "AssignmentId", c => c.Int());
            RenameIndex(table: "dbo.Assignments", name: "IX_SubmitFileId", newName: "IX_SubmitFile_id");
            RenameIndex(table: "dbo.Assignments", name: "IX_TaskFileId", newName: "IX_TaskFile_id");
            RenameColumn(table: "dbo.Assignments", name: "TaskFileId", newName: "TaskFile_id");
            RenameColumn(table: "dbo.Assignments", name: "SubmitFileId", newName: "SubmitFile_id");
            CreateIndex("dbo.TaskFiles", "AssignmentId");
            AddForeignKey("dbo.TaskFiles", "AssignmentId", "dbo.Assignments", "AssignmentId");
        }
    }
}
