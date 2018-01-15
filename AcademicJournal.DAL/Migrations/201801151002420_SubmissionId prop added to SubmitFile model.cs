namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubmissionIdpropaddedtoSubmitFilemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubmitFiles", "SubmissionId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubmitFiles", "SubmissionId");
        }
    }
}
