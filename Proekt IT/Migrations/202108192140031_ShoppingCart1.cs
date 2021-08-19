namespace Proekt_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingCart1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCarts", "proizvod_id", "dbo.Menus");
            DropIndex("dbo.ShoppingCarts", new[] { "proizvod_id" });
            DropTable("dbo.ShoppingCarts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cena = c.Int(nullable: false),
                        proizvod_id = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.ShoppingCarts", "proizvod_id");
            AddForeignKey("dbo.ShoppingCarts", "proizvod_id", "dbo.Menus", "id");
        }
    }
}
