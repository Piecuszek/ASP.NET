namespace Piecyk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Druga : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoModels", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfoModels", "Email");
        }
    }
}
