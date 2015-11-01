namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vendor_BusinessType_Map : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vendor_BusinessType_Map",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        BusinessTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.BusinessTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Vendor", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId)
                .Index(t => t.BusinessTypeId);
            
            AlterColumn("dbo.Vendor", "PreferredShippingCarrier", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendor_BusinessType_Map", "VendorId", "dbo.Vendor");
            DropForeignKey("dbo.Vendor_BusinessType_Map", "BusinessTypeId", "dbo.Category");
            DropIndex("dbo.Vendor_BusinessType_Map", new[] { "BusinessTypeId" });
            DropIndex("dbo.Vendor_BusinessType_Map", new[] { "VendorId" });
            AlterColumn("dbo.Vendor", "PreferredShippingCarrier", c => c.String());
            DropTable("dbo.Vendor_BusinessType_Map");
        }
    }
}
