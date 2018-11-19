namespace FinalDams.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkingMetaToType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MetaAssetTypes",
                c => new
                    {
                        Meta_ID = c.Int(nullable: false),
                        AssetType_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meta_ID, t.AssetType_ID })
                .ForeignKey("dbo.Metas", t => t.Meta_ID, cascadeDelete: true)
                .ForeignKey("dbo.AssetTypes", t => t.AssetType_ID, cascadeDelete: true)
                .Index(t => t.Meta_ID)
                .Index(t => t.AssetType_ID);
            
            DropTable("dbo.Mims");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Mims",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeID = c.Int(nullable: false),
                        MetaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.MetaAssetTypes", "AssetType_ID", "dbo.AssetTypes");
            DropForeignKey("dbo.MetaAssetTypes", "Meta_ID", "dbo.Metas");
            DropIndex("dbo.MetaAssetTypes", new[] { "AssetType_ID" });
            DropIndex("dbo.MetaAssetTypes", new[] { "Meta_ID" });
            DropTable("dbo.MetaAssetTypes");
        }
    }
}
