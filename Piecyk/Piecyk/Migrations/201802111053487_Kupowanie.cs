namespace Piecyk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kupowanie : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CarModels", "SalesDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarModels", "SalesDate", c => c.DateTime());
        }
    }
}
