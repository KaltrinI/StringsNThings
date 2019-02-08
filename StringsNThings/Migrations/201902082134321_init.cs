namespace StringsNThings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "instrument_Id", "dbo.Instruments");
            DropIndex("dbo.CartItems", new[] { "instrument_Id" });
            RenameColumn(table: "dbo.CartItems", name: "instrument_Id", newName: "InstrumentId");
            AddColumn("dbo.Transactions", "InstrumendId", c => c.Int(nullable: false));
            AlterColumn("dbo.CartItems", "InstrumentId", c => c.Int(nullable: false));
            CreateIndex("dbo.CartItems", "InstrumentId");
            CreateIndex("dbo.Transactions", "InstrumendId");
            AddForeignKey("dbo.Transactions", "InstrumendId", "dbo.Instruments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CartItems", "InstrumentId", "dbo.Instruments", "Id", cascadeDelete: true);
            DropColumn("dbo.Transactions", "InstrumentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "InstrumentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CartItems", "InstrumentId", "dbo.Instruments");
            DropForeignKey("dbo.Transactions", "InstrumendId", "dbo.Instruments");
            DropIndex("dbo.Transactions", new[] { "InstrumendId" });
            DropIndex("dbo.CartItems", new[] { "InstrumentId" });
            AlterColumn("dbo.CartItems", "InstrumentId", c => c.Int());
            DropColumn("dbo.Transactions", "InstrumendId");
            RenameColumn(table: "dbo.CartItems", name: "InstrumentId", newName: "instrument_Id");
            CreateIndex("dbo.CartItems", "instrument_Id");
            AddForeignKey("dbo.CartItems", "instrument_Id", "dbo.Instruments", "Id");
        }
    }
}
