namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentmodeliscreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Created = c.DateTime(),
                        Edited = c.DateTime(),
                        IsRead = c.Boolean(),
                        AssignmentId = c.Int(nullable: false),
                        Nesting = c.Int(),
                        ParentCommentId = c.Int(),
                        StudentId = c.String(maxLength: 128),
                        MentorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.MentorId)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId)
                .Index(t => t.AssignmentId)
                .Index(t => t.ParentCommentId)
                .Index(t => t.StudentId)
                .Index(t => t.MentorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "MentorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "AssignmentId", "dbo.Assignments");
            DropIndex("dbo.Comments", new[] { "MentorId" });
            DropIndex("dbo.Comments", new[] { "StudentId" });
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropIndex("dbo.Comments", new[] { "AssignmentId" });
            DropTable("dbo.Comments");
        }
    }
}
