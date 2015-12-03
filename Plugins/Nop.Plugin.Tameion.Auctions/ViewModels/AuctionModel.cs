using Nop.Plugin.Tameion.Auctions.DomainModels;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.Auctions.ViewModels
{
    public class AuctionModel : BaseNopEntityModel //,ILocalizedModel<ProductLocalizedModel>
    {
        public AuctionModel()
        {
            //Locales = new List<ProductLocalizedModel>();
            //ProductPictureModels = new List<ProductPictureModel>();
            //CopyProductModel = new CopyProductModel();
            //AvailableBasepriceUnits = new List<SelectListItem>();
            //AvailableBasepriceBaseUnits = new List<SelectListItem>();
            //AvailableProductTemplates = new List<SelectListItem>();
            AvailableVendors = new List<SelectListItem>();
            //AvailableTaxCategories = new List<SelectListItem>();
            //AvailableDeliveryDates = new List<SelectListItem>();
            //AvailableWarehouses = new List<SelectListItem>();
            //AvailableCategories = new List<SelectListItem>();
            //AvailableManufacturers = new List<SelectListItem>();
            //AvailableProductAttributes = new List<SelectListItem>();
            //AddPictureModel = new ProductPictureModel();
            //AddSpecificationAttributeModel = new AddProductSpecificationAttributeModel();
            //ProductWarehouseInventoryModels = new List<ProductWarehouseInventoryModel>();
        }

        [Required]
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public List<SelectListItem> AvailableVendors { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        [Required]
        public decimal StartingPrice { get; set; }
        [Required]
        public decimal ReservedAmount { get; set; }
        public AuctionStatus Status { get; set; }
        public DateTime StartingDate { get; set; }
        public bool Published { get; set; }
        public string PrimaryStoreCurrencyCode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public partial class AddRelatedProductModel : BaseNopModel
        {
            public AddRelatedProductModel()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableManufacturers = new List<SelectListItem>();
                AvailableStores = new List<SelectListItem>();
                AvailableVendors = new List<SelectListItem>();
                AvailableProductTypes = new List<SelectListItem>();
            }

            [NopResourceDisplayName("Admin.Catalog.Products.List.SearchProductName")]
            [AllowHtml]
            public string SearchProductName { get; set; }
            [NopResourceDisplayName("Admin.Catalog.Products.List.SearchCategory")]
            public int SearchCategoryId { get; set; }
            [NopResourceDisplayName("Admin.Catalog.Products.List.SearchManufacturer")]
            public int SearchManufacturerId { get; set; }
            [NopResourceDisplayName("Admin.Catalog.Products.List.SearchStore")]
            public int SearchStoreId { get; set; }
            [NopResourceDisplayName("Admin.Catalog.Products.List.SearchVendor")]
            public int SearchVendorId { get; set; }
            [NopResourceDisplayName("Admin.Catalog.Products.List.SearchProductType")]
            public int SearchProductTypeId { get; set; }

            public IList<SelectListItem> AvailableCategories { get; set; }
            public IList<SelectListItem> AvailableManufacturers { get; set; }
            public IList<SelectListItem> AvailableStores { get; set; }
            public IList<SelectListItem> AvailableVendors { get; set; }
            public IList<SelectListItem> AvailableProductTypes { get; set; }

            public int AuctionId { get; set; }

            public int[] SelectedProductIds { get; set; }

            //vendor
            public bool IsLoggedInAsVendor { get; set; }
        }
    }
}
