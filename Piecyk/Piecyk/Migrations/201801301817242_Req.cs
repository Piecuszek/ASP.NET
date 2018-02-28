namespace Piecyk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Req : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AdModels", "CarID", c => c.String(nullable: false));
            AlterColumn("dbo.TuningModels", "CarID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TuningModels", "CarID", c => c.String());
            AlterColumn("dbo.AdModels", "CarID", c => c.String());
        }
    }
}
