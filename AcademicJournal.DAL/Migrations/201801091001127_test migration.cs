namespace AcademicJournal.BLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testmigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "MentorId" });
            RenameColumn(table: "dbo.Comments", name: "MentorId", newName: "Author_Id");
            RenameColumn(table: "dbo.Comments", name: "StudentId", newName: "Author_Id");
            RenameIndex(table: "dbo.Comments", name: "IX_StudentId", newName: "IX_Author_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_Author_Id", newName: "IX_StudentId");
            RenameColumn(table: "dbo.Comments", name: "Author_Id", newName: "StudentId");
            RenameColumn(table: "dbo.Comments", name: "Author_Id", newName: "MentorId");
            CreateIndex("dbo.Comments", "MentorId");
        }
    }
}
