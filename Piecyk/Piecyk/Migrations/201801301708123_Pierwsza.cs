namespace Piecyk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pierwsza : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoModels", "PhoneNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfoModels", "PhoneNo");
        }
    }
}
