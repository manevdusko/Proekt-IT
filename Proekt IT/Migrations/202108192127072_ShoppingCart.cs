namespace Proekt_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cena = c.Int(nullable: false),
                        proizvod_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Menus", t => t.proizvod_id)
                .Index(t => t.proizvod_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCarts", "proizvod_id", "dbo.Menus");
            DropIndex("dbo.ShoppingCarts", new[] { "proizvod_id" });
            DropTable("dbo.ShoppingCarts");
        }
    }
}
