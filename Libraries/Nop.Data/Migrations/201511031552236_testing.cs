namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testing : DbMigration
    {
        public override void Up()
        {
            // The below code is already being applied by plugin, comment it
            //RenameTable(name: "dbo.Vendor_BusinessType_Map", newName: "VendorBusinessTypes");
            //CreateTable(
            //    "dbo.VendorPayoutMethods",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            VendorId = c.Int(nullable: false),
            //            PayoutMethodId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.PayoutMethods", t => t.PayoutMethodId, cascadeDelete: true)
            //    .ForeignKey("dbo.Vendor", t => t.VendorId, cascadeDelete: true)
            //    .Index(t => t.VendorId)
            //    .Index(t => t.PayoutMethodId);
            
            //CreateTable(
            //    "dbo.PayoutMethods",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.VendorPayoutMethods", "VendorId", "dbo.Vendor");
            //DropForeignKey("dbo.VendorPayoutMethods", "PayoutMethodId", "dbo.PayoutMethods");
            //DropIndex("dbo.VendorPayoutMethods", new[] { "PayoutMethodId" });
            //DropIndex("dbo.VendorPayoutMethods", new[] { "VendorId" });
            //DropTable("dbo.PayoutMethods");
            //DropTable("dbo.VendorPayoutMethods");
            //RenameTable(name: "dbo.VendorBusinessTypes", newName: "Vendor_BusinessType_Map");
        }
    }
}
