namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VendorModelPersonalInfoFieldsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendor", "Password", c => c.String());
            AddColumn("dbo.Vendor", "EnableGoogleAnalytics", c => c.Boolean(nullable: false));
            AddColumn("dbo.Vendor", "GoogleAnalyticsAccountNumber", c => c.String());
            AddColumn("dbo.Vendor", "PreferredShippingCarrier", c => c.String());
            AddColumn("dbo.Vendor", "PreferredSubdomainName", c => c.String());
            AddColumn("dbo.Vendor", "AttentionTo", c => c.String());
            AddColumn("dbo.Vendor", "StreetAddressLine1", c => c.String());
            AddColumn("dbo.Vendor", "StreetAddressLine2", c => c.String());
            AddColumn("dbo.Vendor", "ZipPostalCode", c => c.String());
            AddColumn("dbo.Vendor", "City", c => c.String());
            AddColumn("dbo.Vendor", "StateProvince", c => c.String());
            AddColumn("dbo.Vendor", "Country", c => c.String());
            AddColumn("dbo.Vendor", "LogoImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendor", "LogoImage");
            DropColumn("dbo.Vendor", "Country");
            DropColumn("dbo.Vendor", "StateProvince");
            DropColumn("dbo.Vendor", "City");
            DropColumn("dbo.Vendor", "ZipPostalCode");
            DropColumn("dbo.Vendor", "StreetAddressLine2");
            DropColumn("dbo.Vendor", "StreetAddressLine1");
            DropColumn("dbo.Vendor", "AttentionTo");
            DropColumn("dbo.Vendor", "PreferredSubdomainName");
            DropColumn("dbo.Vendor", "PreferredShippingCarrier");
            DropColumn("dbo.Vendor", "GoogleAnalyticsAccountNumber");
            DropColumn("dbo.Vendor", "EnableGoogleAnalytics");
            DropColumn("dbo.Vendor", "Password");
        }
    }
}
