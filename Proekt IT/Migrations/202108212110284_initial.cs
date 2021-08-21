namespace Proekt_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        imgUrl = c.String(),
                        imeNaJadenje = c.String(),
                        sostojki = c.String(),
                        cena = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        productId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        orders = c.String(),
                        brojNaGosti = c.Int(nullable: false),
                        sum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Vrabotens",
                c => new
                    {
                        email = c.String(nullable: false, maxLength: 128),
                        promet = c.Int(nullable: false),
                        najava = c.Time(nullable: false, precision: 7),
                        odjava = c.Time(nullable: false, precision: 7),
                        raboteno = c.Time(nullable: false, precision: 7),
                        plata = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.email);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vrabotens");
            DropTable("dbo.Tables");
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.Menus");
        }
    }
}
