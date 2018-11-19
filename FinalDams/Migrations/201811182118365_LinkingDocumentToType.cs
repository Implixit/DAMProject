namespace FinalDams.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkingDocumentToType : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Types", newName: "AssetTypes");
            AddColumn("dbo.Documents", "AssetType_ID", c => c.Int());
            CreateIndex("dbo.Documents", "AssetType_ID");
            AddForeignKey("dbo.Documents", "AssetType_ID", "dbo.AssetTypes", "ID");
            DropColumn("dbo.Documents", "TypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "TypeID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Documents", "AssetType_ID", "dbo.AssetTypes");
            DropIndex("dbo.Documents", new[] { "AssetType_ID" });
            DropColumn("dbo.Documents", "AssetType_ID");
            RenameTable(name: "dbo.AssetTypes", newName: "Types");
        }
    }
}
