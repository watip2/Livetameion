using Nop.Core;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.GroupDeals.Enums;
using Nop.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupDeals.Models
{
    public class GroupDeal : BaseEntity, /*ILocalizedEntity*/ ISlugSupported/*, IAclSupported, IStoreMappingSupported*/
    {
        //private ICollection<ProductCategory> _productCategories;
        //private ICollection<ProductManufacturer> _productManufacturers;
        //private ICollection<ProductPicture> _productPictures;
        //private ICollection<ProductReview> _productReviews;
        //private ICollection<ProductSpecificationAttribute> _productSpecificationAttributes;
        //private ICollection<ProductTag> _productTags;
        //private ICollection<ProductAttributeMapping> _productAttributeMappings;
        //private ICollection<ProductAttributeCombination> _productAttributeCombinations;
        //private ICollection<TierPrice> _tierPrices;
        //private ICollection<Discount> _appliedDiscounts;
        //private ICollection<ProductWarehouseInventory> _productWarehouseInventory;
        
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public int VendorId { get;set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
        public int DisplayOrder { get; set; }
        public string SeName { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool Published { get; set; }
        public string CouponCode { get; set; }
        public DateTime SpecialPriceStartDateTimeUtc { get; set; }
        public DateTime SpecialPriceEndDateTimeUtc { get; set; }
        
        // generic attributes
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        [AllowHtml]
        public string ShortDescription { get; set; }
        [NotMapped]
        [AllowHtml]
        public string FullDescription { get; set; }
        [NotMapped]
        public bool AllowBackInStockSubscriptions { get; set; }
        [NotMapped]
        public DateTime AvailableStartDateTimeUtc { get; set; }
        [NotMapped]
        public DateTime AvailableEndDateTimeUtc { get; set; }
        [NotMapped]
        public string Country { get; set; }
        [NotMapped]
        public string StateOrProvince { get; set; }
        [NotMapped]
        public string City { get; set; }
        [NotMapped]
        public decimal Price { get; set; }
        [NotMapped]
        public decimal OldPrice { get; set; }
        [NotMapped]
        public decimal GroupDealCost { get; set; }
        [NotMapped]
        public decimal SpecialPrice { get; set; }
        [NotMapped]
        public int StockQuantity { get; set; }
        [NotMapped]
        public bool DisplayStockAvailability { get; set; }
        [NotMapped]
        public bool DisplayStockQuantity { get; set; }
        [NotMapped]
        public int MinStockQuantity { get; set; }

        //public ProductType ProductType
        //{
        //    get
        //    {
        //        return (ProductType)this.ProductTypeId;
        //    }
        //    set
        //    {
        //        this.ProductTypeId = (int)value;
        //    }
        //}

        //public BackorderMode BackorderMode
        //{
        //    get
        //    {
        //        return (BackorderMode)this.BackorderModeId;
        //    }
        //    set
        //    {
        //        this.BackorderModeId = (int)value;
        //    }
        //}

        //public DownloadActivationType DownloadActivationType
        //{
        //    get
        //    {
        //        return (DownloadActivationType)this.DownloadActivationTypeId;
        //    }
        //    set
        //    {
        //        this.DownloadActivationTypeId = (int)value;
        //    }
        //}

        //public GiftCardType GiftCardType
        //{
        //    get
        //    {
        //        return (GiftCardType)this.GiftCardTypeId;
        //    }
        //    set
        //    {
        //        this.GiftCardTypeId = (int)value;
        //    }
        //}

        //public LowStockActivity LowStockActivity
        //{
        //    get
        //    {
        //        return (LowStockActivity)this.LowStockActivityId;
        //    }
        //    set
        //    {
        //        this.LowStockActivityId = (int)value;
        //    }
        //}

        //public ManageInventoryMethod ManageInventoryMethod
        //{
        //    get
        //    {
        //        return (ManageInventoryMethod)this.ManageInventoryMethodId;
        //    }
        //    set
        //    {
        //        this.ManageInventoryMethodId = (int)value;
        //    }
        //}

        //public RecurringProductCyclePeriod RecurringCyclePeriod
        //{
        //    get
        //    {
        //        return (RecurringProductCyclePeriod)this.RecurringCyclePeriodId;
        //    }
        //    set
        //    {
        //        this.RecurringCyclePeriodId = (int)value;
        //    }
        //}

        //public RentalPricePeriod RentalPricePeriod
        //{
        //    get
        //    {
        //        return (RentalPricePeriod)this.RentalPricePeriodId;
        //    }
        //    set
        //    {
        //        this.RentalPricePeriodId = (int)value;
        //    }
        //}

        //public virtual ICollection<ProductCategory> ProductCategories
        //{
        //    get { return _productCategories ?? (_productCategories = new List<ProductCategory>()); }
        //    protected set { _productCategories = value; }
        //}

        //public virtual ICollection<ProductManufacturer> ProductManufacturers
        //{
        //    get { return _productManufacturers ?? (_productManufacturers = new List<ProductManufacturer>()); }
        //    protected set { _productManufacturers = value; }
        //}

        //public virtual ICollection<ProductPicture> ProductPictures
        //{
        //    get { return _productPictures ?? (_productPictures = new List<ProductPicture>()); }
        //    protected set { _productPictures = value; }
        //}

        //public virtual ICollection<ProductReview> ProductReviews
        //{
        //    get { return _productReviews ?? (_productReviews = new List<ProductReview>()); }
        //    protected set { _productReviews = value; }
        //}

        //public virtual ICollection<ProductSpecificationAttribute> ProductSpecificationAttributes
        //{
        //    get { return _productSpecificationAttributes ?? (_productSpecificationAttributes = new List<ProductSpecificationAttribute>()); }
        //    protected set { _productSpecificationAttributes = value; }
        //}

        //public virtual ICollection<ProductTag> ProductTags
        //{
        //    get { return _productTags ?? (_productTags = new List<ProductTag>()); }
        //    protected set { _productTags = value; }
        //}

        //public virtual ICollection<ProductAttributeMapping> ProductAttributeMappings
        //{
        //    get { return _productAttributeMappings ?? (_productAttributeMappings = new List<ProductAttributeMapping>()); }
        //    protected set { _productAttributeMappings = value; }
        //}

        //public virtual ICollection<ProductAttributeCombination> ProductAttributeCombinations
        //{
        //    get { return _productAttributeCombinations ?? (_productAttributeCombinations = new List<ProductAttributeCombination>()); }
        //    protected set { _productAttributeCombinations = value; }
        //}

        //public virtual ICollection<TierPrice> TierPrices
        //{
        //    get { return _tierPrices ?? (_tierPrices = new List<TierPrice>()); }
        //    protected set { _tierPrices = value; }
        //}

        //public virtual ICollection<Discount> AppliedDiscounts
        //{
        //    get { return _appliedDiscounts ?? (_appliedDiscounts = new List<Discount>()); }
        //    protected set { _appliedDiscounts = value; }
        //}

        //public virtual ICollection<ProductWarehouseInventory> ProductWarehouseInventory
        //{
        //    get { return _productWarehouseInventory ?? (_productWarehouseInventory = new List<ProductWarehouseInventory>()); }
        //    protected set { _productWarehouseInventory = value; }
        //}
    }
}