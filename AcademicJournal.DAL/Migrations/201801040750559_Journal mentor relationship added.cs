namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Journalmentorrelationshipadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Journals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        MentorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.MentorId)
                .Index(t => t.MentorId);
            
            AddColumn("dbo.WorkDays", "Journal_Id", c => c.Int());
            CreateIndex("dbo.WorkDays", "Journal_Id");
            AddForeignKey("dbo.WorkDays", "Journal_Id", "dbo.Journals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Journals", "MentorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkDays", "Journal_Id", "dbo.Journals");
            DropIndex("dbo.WorkDays", new[] { "Journal_Id" });
            DropIndex("dbo.Journals", new[] { "MentorId" });
            DropColumn("dbo.WorkDays", "Journal_Id");
            DropTable("dbo.Journals");
        }
    }
}
