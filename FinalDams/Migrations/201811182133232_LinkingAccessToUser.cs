namespace FinalDams.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkingAccessToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ACL_ID", c => c.Int());
            CreateIndex("dbo.Users", "ACL_ID");
            AddForeignKey("dbo.Users", "ACL_ID", "dbo.Accesses", "ID");
            DropColumn("dbo.Users", "AccessID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "AccessID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Users", "ACL_ID", "dbo.Accesses");
            DropIndex("dbo.Users", new[] { "ACL_ID" });
            DropColumn("dbo.Users", "ACL_ID");
        }
    }
}
