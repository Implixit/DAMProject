namespace FinalDams.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkingMetaToData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Data", "MetaType_ID", c => c.Int());
            CreateIndex("dbo.Data", "MetaType_ID");
            AddForeignKey("dbo.Data", "MetaType_ID", "dbo.Metas", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Data", "MetaType_ID", "dbo.Metas");
            DropIndex("dbo.Data", new[] { "MetaType_ID" });
            DropColumn("dbo.Data", "MetaType_ID");
        }
    }
}
