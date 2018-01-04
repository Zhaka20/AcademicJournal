namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkDaymodelcreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Attendances", "WorkDayId", c => c.Int(nullable: false));
            CreateIndex("dbo.Attendances", "WorkDayId");
            AddForeignKey("dbo.Attendances", "WorkDayId", "dbo.WorkDays", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "WorkDayId", "dbo.WorkDays");
            DropIndex("dbo.Attendances", new[] { "WorkDayId" });
            DropColumn("dbo.Attendances", "WorkDayId");
            DropTable("dbo.WorkDays");
        }
    }
}
