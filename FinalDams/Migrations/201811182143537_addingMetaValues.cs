namespace FinalDams.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingMetaValues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Data", "MetaValue", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Data", "MetaValue");
        }
    }
}
