namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascadedeletetest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "SubmitFileId", "dbo.TaskFiles");
            DropForeignKey("dbo.Assignments", "TaskFileId", "dbo.TaskFiles");
            DropIndex("dbo.Assignments", new[] { "TaskFileId" });
            DropIndex("dbo.Assignments", new[] { "SubmitFileId" });
            RenameColumn(table: "dbo.TaskFiles", name: "SubmitFileId", newName: "Assignment_AssignmentId1");
            DropPrimaryKey("dbo.TaskFiles");
            AddColumn("dbo.TaskFiles", "TaskFileId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.TaskFiles", "AssignmentId", c => c.Int());
            AddColumn("dbo.TaskFiles", "Assignment_AssignmentId2", c => c.Int());
            AddPrimaryKey("dbo.TaskFiles", "TaskFileId");
            CreateIndex("dbo.TaskFiles", "Assignment_AssignmentId1");
            CreateIndex("dbo.TaskFiles", "Assignment_AssignmentId2");
            AddForeignKey("dbo.TaskFiles", "Assignment_AssignmentId1", "dbo.Assignments", "AssignmentId", cascadeDelete: true);
            AddForeignKey("dbo.TaskFiles", "Assignment_AssignmentId2", "dbo.Assignments", "AssignmentId", cascadeDelete: true);
            DropColumn("dbo.TaskFiles", "id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskFiles", "id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.TaskFiles", "Assignment_AssignmentId2", "dbo.Assignments");
            DropForeignKey("dbo.TaskFiles", "Assignment_AssignmentId1", "dbo.Assignments");
            DropIndex("dbo.TaskFiles", new[] { "Assignment_AssignmentId2" });
            DropIndex("dbo.TaskFiles", new[] { "Assignment_AssignmentId1" });
            DropPrimaryKey("dbo.TaskFiles");
            DropColumn("dbo.TaskFiles", "Assignment_AssignmentId2");
            DropColumn("dbo.TaskFiles", "AssignmentId");
            DropColumn("dbo.TaskFiles", "TaskFileId");
            AddPrimaryKey("dbo.TaskFiles", "id");
            RenameColumn(table: "dbo.TaskFiles", name: "Assignment_AssignmentId1", newName: "SubmitFileId");
            CreateIndex("dbo.Assignments", "SubmitFileId");
            CreateIndex("dbo.Assignments", "TaskFileId");
            AddForeignKey("dbo.Assignments", "TaskFileId", "dbo.TaskFiles", "id");
            AddForeignKey("dbo.Assignments", "SubmitFileId", "dbo.TaskFiles", "id");
        }
    }
}
