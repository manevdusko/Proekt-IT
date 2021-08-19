namespace Proekt_IT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisplayImageInMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "imgUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "imgUrl");
        }
    }
}
