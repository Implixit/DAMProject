namespace FinalDams.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accessLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accesses", "usertype", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accesses", "usertype");
        }
    }
}
