namespace Piecyk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ads : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdModels", "Car_ID", "dbo.CarModels");
            DropIndex("dbo.AdModels", new[] { "Car_ID" });
            AddColumn("dbo.CarModels", "Status", c => c.Boolean(nullable: false));
            DropTable("dbo.AdModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AdModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CarID = c.String(nullable: false),
                        status = c.Boolean(nullable: false),
                        Car_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.CarModels", "Status");
            CreateIndex("dbo.AdModels", "Car_ID");
            AddForeignKey("dbo.AdModels", "Car_ID", "dbo.CarModels", "ID");
        }
    }
}
