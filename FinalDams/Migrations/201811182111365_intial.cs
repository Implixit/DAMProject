namespace FinalDams.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accesses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccessLevel = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Data",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DocumentID = c.Int(nullable: false),
                        MetaID = c.Int(nullable: false),
                        MetaField = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UploadDate = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Metas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FieldName = c.String(),
                        FieldType = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Mims",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeID = c.Int(nullable: false),
                        MetaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AccessID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        AccessID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Types");
            DropTable("dbo.Mims");
            DropTable("dbo.Metas");
            DropTable("dbo.Documents");
            DropTable("dbo.Data");
            DropTable("dbo.Accesses");
        }
    }
}
