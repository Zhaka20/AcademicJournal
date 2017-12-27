namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskFilemodelcreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskFiles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UploadFile = c.String(),
                        FileName = c.String(),
                        Assignment_AssignmentId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Assignments", t => t.Assignment_AssignmentId)
                .Index(t => t.Assignment_AssignmentId);
            
            AddColumn("dbo.Assignments", "SubmitFile_id", c => c.Int());
            AddColumn("dbo.Assignments", "TaskFile_id", c => c.Int());
            CreateIndex("dbo.Assignments", "SubmitFile_id");
            CreateIndex("dbo.Assignments", "TaskFile_id");
            AddForeignKey("dbo.Assignments", "SubmitFile_id", "dbo.TaskFiles", "id");
            AddForeignKey("dbo.Assignments", "TaskFile_id", "dbo.TaskFiles", "id");
            DropColumn("dbo.Assignments", "UploadFile");
            DropColumn("dbo.Assignments", "FileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assignments", "FileName", c => c.String());
            AddColumn("dbo.Assignments", "UploadFile", c => c.String());
            DropForeignKey("dbo.Assignments", "TaskFile_id", "dbo.TaskFiles");
            DropForeignKey("dbo.Assignments", "SubmitFile_id", "dbo.TaskFiles");
            DropForeignKey("dbo.TaskFiles", "Assignment_AssignmentId", "dbo.Assignments");
            DropIndex("dbo.TaskFiles", new[] { "Assignment_AssignmentId" });
            DropIndex("dbo.Assignments", new[] { "TaskFile_id" });
            DropIndex("dbo.Assignments", new[] { "SubmitFile_id" });
            DropColumn("dbo.Assignments", "TaskFile_id");
            DropColumn("dbo.Assignments", "SubmitFile_id");
            DropTable("dbo.TaskFiles");
        }
    }
}
