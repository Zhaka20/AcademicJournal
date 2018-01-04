namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JournalworDayrelationshipfixed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkDays", "Journal_Id", "dbo.Journals");
            DropIndex("dbo.WorkDays", new[] { "Journal_Id" });
            RenameColumn(table: "dbo.WorkDays", name: "Journal_Id", newName: "JournalId");
            AlterColumn("dbo.WorkDays", "JournalId", c => c.Int(nullable: false));
            CreateIndex("dbo.WorkDays", "JournalId");
            AddForeignKey("dbo.WorkDays", "JournalId", "dbo.Journals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkDays", "JournalId", "dbo.Journals");
            DropIndex("dbo.WorkDays", new[] { "JournalId" });
            AlterColumn("dbo.WorkDays", "JournalId", c => c.Int());
            RenameColumn(table: "dbo.WorkDays", name: "JournalId", newName: "Journal_Id");
            CreateIndex("dbo.WorkDays", "Journal_Id");
            AddForeignKey("dbo.WorkDays", "Journal_Id", "dbo.Journals", "Id");
        }
    }
}
