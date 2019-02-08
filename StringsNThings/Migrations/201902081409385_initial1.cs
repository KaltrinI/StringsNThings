namespace StringsNThings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instruments", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instruments", "Image");
        }
    }
}
