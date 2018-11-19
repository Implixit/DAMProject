namespace FinalDams.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkingDocumentToData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Data", "Document_ID", c => c.Int());
            CreateIndex("dbo.Data", "Document_ID");
            AddForeignKey("dbo.Data", "Document_ID", "dbo.Documents", "ID");
            DropColumn("dbo.Data", "DocumentID");
            DropColumn("dbo.Data", "MetaID");
            DropColumn("dbo.Data", "MetaField");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Data", "MetaField", c => c.String());
            AddColumn("dbo.Data", "MetaID", c => c.Int(nullable: false));
            AddColumn("dbo.Data", "DocumentID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Data", "Document_ID", "dbo.Documents");
            DropIndex("dbo.Data", new[] { "Document_ID" });
            DropColumn("dbo.Data", "Document_ID");
        }
    }
}
