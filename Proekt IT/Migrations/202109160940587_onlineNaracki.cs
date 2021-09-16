namespace Proekt_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class onlineNaracki : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OnlineNarackis",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        naracka = c.String(),
                        adresa = c.String(),
                        cena = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OnlineNarackis");
        }
    }
}
