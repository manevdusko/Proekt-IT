namespace Proekt_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vraboteni : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vrabotens",
                c => new
                    {
                        email = c.String(nullable: false, maxLength: 128),
                        promet = c.Int(nullable: false),
                        najava = c.Time(nullable: false, precision: 7),
                        odjava = c.Time(nullable: false, precision: 7),
                        raboteno = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.email);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vrabotens");
        }
    }
}
