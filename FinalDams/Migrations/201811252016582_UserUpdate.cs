namespace FinalDams.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "UserID_ID", c => c.Int());
            CreateIndex("dbo.Documents", "UserID_ID");
            AddForeignKey("dbo.Documents", "UserID_ID", "dbo.Users", "ID");
            DropColumn("dbo.Documents", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "UserID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Documents", "UserID_ID", "dbo.Users");
            DropIndex("dbo.Documents", new[] { "UserID_ID" });
            DropColumn("dbo.Documents", "UserID_ID");
        }
    }
}
