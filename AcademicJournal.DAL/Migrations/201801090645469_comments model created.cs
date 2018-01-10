namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentsmodelcreated : DbMigration
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
                        AuthorId = c.String(maxLength: 128),
                        ParentCommentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .Index(t => t.AssignmentId)
                .Index(t => t.AuthorId)
                .Index(t => t.ParentCommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "AssignmentId", "dbo.Assignments");
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropIndex("dbo.Comments", new[] { "AssignmentId" });
            DropTable("dbo.Comments");
        }
    }
}
