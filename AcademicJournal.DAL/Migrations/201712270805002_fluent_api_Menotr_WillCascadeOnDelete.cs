namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fluent_api_Menotr_WillCascadeOnDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "CreatorId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Assignments", "CreatorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "CreatorId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Assignments", "CreatorId", "dbo.AspNetUsers", "Id");
        }
    }
}
