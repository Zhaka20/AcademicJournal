namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assignment_model_FileName_field_added_Description_removed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "FileName", c => c.String());
            DropColumn("dbo.Assignments", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assignments", "Description", c => c.String());
            DropColumn("dbo.Assignments", "FileName");
        }
    }
}
