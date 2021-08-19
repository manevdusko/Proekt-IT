namespace Proekt_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListOfOrders1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tables", "orders", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tables", "orders");
        }
    }
}
