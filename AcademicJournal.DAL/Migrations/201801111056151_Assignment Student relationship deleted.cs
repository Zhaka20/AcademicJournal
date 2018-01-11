namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignmentStudentrelationshipdeleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentAssignments", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentAssignments", "Assignment_AssignmentId", "dbo.Assignments");
            DropIndex("dbo.StudentAssignments", new[] { "Student_Id" });
            DropIndex("dbo.StudentAssignments", new[] { "Assignment_AssignmentId" });
            DropTable("dbo.StudentAssignments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StudentAssignments",
                c => new
                    {
                        Student_Id = c.String(nullable: false, maxLength: 128),
                        Assignment_AssignmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Assignment_AssignmentId });
            
            CreateIndex("dbo.StudentAssignments", "Assignment_AssignmentId");
            CreateIndex("dbo.StudentAssignments", "Student_Id");
            AddForeignKey("dbo.StudentAssignments", "Assignment_AssignmentId", "dbo.Assignments", "AssignmentId", cascadeDelete: true);
            AddForeignKey("dbo.StudentAssignments", "Student_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
