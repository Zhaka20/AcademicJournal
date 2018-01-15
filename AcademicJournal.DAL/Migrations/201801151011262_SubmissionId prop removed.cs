namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubmissionIdpropremoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SubmitFiles", "SubmissionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubmitFiles", "SubmissionId", c => c.Int(nullable: false));
        }
    }
}
