namespace Proekt_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListOfOrders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Menus", "Table_id", "dbo.Tables");
            DropIndex("dbo.Menus", new[] { "Table_id" });
            DropColumn("dbo.Menus", "Table_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menus", "Table_id", c => c.Int());
            CreateIndex("dbo.Menus", "Table_id");
            AddForeignKey("dbo.Menus", "Table_id", "dbo.Tables", "id");
        }
    }
}
