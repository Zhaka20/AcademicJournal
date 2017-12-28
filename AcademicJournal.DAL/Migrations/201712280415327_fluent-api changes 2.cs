namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fluentapichanges2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "CreatorId", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "CreatorId" });
            AddColumn("dbo.Assignments", "Mentor_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Assignments", "Creator_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Assignments", "CreatorId", c => c.String());
            CreateIndex("dbo.Assignments", "Mentor_Id");
            CreateIndex("dbo.Assignments", "Creator_Id");
            AddForeignKey("dbo.Assignments", "Creator_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Assignments", "Mentor_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "Mentor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "Creator_Id" });
            DropIndex("dbo.Assignments", new[] { "Mentor_Id" });
            AlterColumn("dbo.Assignments", "CreatorId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Assignments", "Creator_Id");
            DropColumn("dbo.Assignments", "Mentor_Id");
            CreateIndex("dbo.Assignments", "CreatorId");
            AddForeignKey("dbo.Assignments", "CreatorId", "dbo.AspNetUsers", "Id");
        }
    }
}
