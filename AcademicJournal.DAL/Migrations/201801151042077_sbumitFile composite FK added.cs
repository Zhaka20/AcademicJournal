namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sbumitFilecompositeFKadded : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SubmitFiles", name: "Submission_AssignmentId", newName: "AssignmentId");
            RenameColumn(table: "dbo.SubmitFiles", name: "Submission_StudentId", newName: "StudentId");
            RenameIndex(table: "dbo.SubmitFiles", name: "IX_Submission_AssignmentId_Submission_StudentId", newName: "IX_AssignmentId_StudentId");
            DropPrimaryKey("dbo.SubmitFiles");
            AlterColumn("dbo.SubmitFiles", "id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SubmitFiles", new[] { "AssignmentId", "StudentId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SubmitFiles");
            AlterColumn("dbo.SubmitFiles", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.SubmitFiles", "id");
            RenameIndex(table: "dbo.SubmitFiles", name: "IX_AssignmentId_StudentId", newName: "IX_Submission_AssignmentId_Submission_StudentId");
            RenameColumn(table: "dbo.SubmitFiles", name: "StudentId", newName: "Submission_StudentId");
            RenameColumn(table: "dbo.SubmitFiles", name: "AssignmentId", newName: "Submission_AssignmentId");
        }
    }
}
