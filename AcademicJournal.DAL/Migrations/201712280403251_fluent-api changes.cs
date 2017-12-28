namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fluentapichanges : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Assignments", new[] { "CreatorId" });
            AlterColumn("dbo.Assignments", "CreatorId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Assignments", "CreatorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Assignments", new[] { "CreatorId" });
            AlterColumn("dbo.Assignments", "CreatorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Assignments", "CreatorId");
        }
    }
}
