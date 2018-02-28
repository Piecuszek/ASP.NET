namespace Piecyk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kontroller : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdModels", "status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdModels", "status");
        }
    }
}
