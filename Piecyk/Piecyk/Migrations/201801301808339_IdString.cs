namespace Piecyk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdModels", "CarID", "dbo.CarModels");
            DropForeignKey("dbo.TuningModels", "CarID", "dbo.CarModels");
            DropIndex("dbo.AdModels", new[] { "CarID" });
            DropIndex("dbo.TuningModels", new[] { "CarID" });
            AddColumn("dbo.AdModels", "Car_ID", c => c.Int());
            AddColumn("dbo.TuningModels", "Car_ID", c => c.Int());
            AlterColumn("dbo.AdModels", "CarID", c => c.String());
            AlterColumn("dbo.CarModels", "UserID", c => c.String());
            AlterColumn("dbo.TuningModels", "CarID", c => c.String());
            CreateIndex("dbo.AdModels", "Car_ID");
            CreateIndex("dbo.TuningModels", "Car_ID");
            AddForeignKey("dbo.AdModels", "Car_ID", "dbo.CarModels", "ID");
            AddForeignKey("dbo.TuningModels", "Car_ID", "dbo.CarModels", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TuningModels", "Car_ID", "dbo.CarModels");
            DropForeignKey("dbo.AdModels", "Car_ID", "dbo.CarModels");
            DropIndex("dbo.TuningModels", new[] { "Car_ID" });
            DropIndex("dbo.AdModels", new[] { "Car_ID" });
            AlterColumn("dbo.TuningModels", "CarID", c => c.Int(nullable: false));
            AlterColumn("dbo.CarModels", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.AdModels", "CarID", c => c.Int(nullable: false));
            DropColumn("dbo.TuningModels", "Car_ID");
            DropColumn("dbo.AdModels", "Car_ID");
            CreateIndex("dbo.TuningModels", "CarID");
            CreateIndex("dbo.AdModels", "CarID");
            AddForeignKey("dbo.TuningModels", "CarID", "dbo.CarModels", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AdModels", "CarID", "dbo.CarModels", "ID", cascadeDelete: true);
        }
    }
}
