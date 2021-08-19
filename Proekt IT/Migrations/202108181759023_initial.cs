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
                        imeNaJadenje = c.String(),
                        sostojki = c.String(),
                        cena = c.Int(nullable: false),
                        Table_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Tables", t => t.Table_id)
                .Index(t => t.Table_id);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        brojNaGosti = c.Int(nullable: false),
                        sum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menus", "Table_id", "dbo.Tables");
            DropIndex("dbo.Menus", new[] { "Table_id" });
            DropTable("dbo.Tables");
            DropTable("dbo.Menus");
        }
    }
}
