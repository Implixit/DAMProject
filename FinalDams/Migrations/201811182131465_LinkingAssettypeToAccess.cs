namespace FinalDams.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkingAssettypeToAccess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssetTypes", "ACL_ID", c => c.Int());
            CreateIndex("dbo.AssetTypes", "ACL_ID");
            AddForeignKey("dbo.AssetTypes", "ACL_ID", "dbo.Accesses", "ID");
            DropColumn("dbo.Accesses", "TypeID");
            DropColumn("dbo.AssetTypes", "AccessID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AssetTypes", "AccessID", c => c.Int(nullable: false));
            AddColumn("dbo.Accesses", "TypeID", c => c.Int(nullable: false));
            DropForeignKey("dbo.AssetTypes", "ACL_ID", "dbo.Accesses");
            DropIndex("dbo.AssetTypes", new[] { "ACL_ID" });
            DropColumn("dbo.AssetTypes", "ACL_ID");
        }
    }
}
