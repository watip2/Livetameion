using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.GroupDeals.ViewModels
{
    public class GroupDealDetailsModel : ProductDetailsModel
    {

        //public partial class ProductBreadcrumbModel : BaseNopModel
        //{
        //    public ProductBreadcrumbModel()
        //    {
        //        CategoryBreadcrumb = new List<CategorySimpleModel>();
        //    }

        //    public bool Enabled { get; set; }
        //    public int ProductId { get; set; }
        //    public string ProductName { get; set; }
        //    public string ProductSeName { get; set; }
        //    public IList<CategorySimpleModel> CategoryBreadcrumb { get; set; }
        //}

        //public partial class AddToCartModel : BaseNopModel
        //{
        //    public AddToCartModel()
        //    {
        //        this.AllowedQuantities = new List<SelectListItem>();
        //    }
        //    public int ProductId { get; set; }

        //    [NopResourceDisplayName("Products.Qty")]
        //    public int EnteredQuantity { get; set; }

        //    [NopResourceDisplayName("Products.EnterProductPrice")]
        //    public bool CustomerEntersPrice { get; set; }
        //    [NopResourceDisplayName("Products.EnterProductPrice")]
        //    public decimal CustomerEnteredPrice { get; set; }
        //    public String CustomerEnteredPriceRange { get; set; }

        //    public bool DisableBuyButton { get; set; }
        //    public bool DisableWishlistButton { get; set; }
        //    public List<SelectListItem> AllowedQuantities { get; set; }

        //    //rental
        //    public bool IsRental { get; set; }

        //    //pre-order
        //    public bool AvailableForPreOrder { get; set; }
        //    public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }

        //    //updating existing shopping cart item?
        //    public int UpdatedShoppingCartItemId { get; set; }
        //}

        //public partial class ProductPriceModel : BaseNopModel
        //{
        //    /// <summary>
        //    /// The currency (in 3-letter ISO 4217 format) of the offer price 
        //    /// </summary>
        //    public string CurrencyCode { get; set; }

        //    public string OldPrice { get; set; }

        //    public string Price { get; set; }
        //    public string PriceWithDiscount { get; set; }

        //    public decimal PriceValue { get; set; }
        //    public decimal PriceWithDiscountValue { get; set; }

        //    public bool CustomerEntersPrice { get; set; }

        //    public bool CallForPrice { get; set; }

        //    public int ProductId { get; set; }

        //    public bool HidePrices { get; set; }

        //    //rental
        //    public bool IsRental { get; set; }
        //    public string RentalPrice { get; set; }

        //    /// <summary>
        //    /// A value indicating whether we should display tax/shipping info (used in Germany)
        //    /// </summary>
        //    public bool DisplayTaxShippingInfo { get; set; }
        //    /// <summary>
        //    /// PAngV baseprice (used in Germany)
        //    /// </summary>
        //    public string BasePricePAngV { get; set; }
        //}

        //public partial class GiftCardModel : BaseNopModel
        //{
        //    public bool IsGiftCard { get; set; }

        //    [NopResourceDisplayName("Products.GiftCard.RecipientName")]
        //    [AllowHtml]
        //    public string RecipientName { get; set; }
        //    [NopResourceDisplayName("Products.GiftCard.RecipientEmail")]
        //    [AllowHtml]
        //    public string RecipientEmail { get; set; }
        //    [NopResourceDisplayName("Products.GiftCard.SenderName")]
        //    [AllowHtml]
        //    public string SenderName { get; set; }
        //    [NopResourceDisplayName("Products.GiftCard.SenderEmail")]
        //    [AllowHtml]
        //    public string SenderEmail { get; set; }
        //    [NopResourceDisplayName("Products.GiftCard.Message")]
        //    [AllowHtml]
        //    public string Message { get; set; }

        //    public GiftCardType GiftCardType { get; set; }
        //}

        //public partial class TierPriceModel : BaseNopModel
        //{
        //    public string Price { get; set; }

        //    public int Quantity { get; set; }
        //}

        //public partial class ProductAttributeModel : BaseNopEntityModel
        //{
        //    public ProductAttributeModel()
        //    {
        //        AllowedFileExtensions = new List<string>();
        //        Values = new List<ProductAttributeValueModel>();
        //    }

        //    public int ProductId { get; set; }

        //    public int ProductAttributeId { get; set; }

        //    public string Name { get; set; }

        //    public string Description { get; set; }

        //    public string TextPrompt { get; set; }

        //    public bool IsRequired { get; set; }

        //    /// <summary>
        //    /// Default value for textboxes
        //    /// </summary>
        //    public string DefaultValue { get; set; }
        //    /// <summary>
        //    /// Selected day value for datepicker
        //    /// </summary>
        //    public int? SelectedDay { get; set; }
        //    /// <summary>
        //    /// Selected month value for datepicker
        //    /// </summary>
        //    public int? SelectedMonth { get; set; }
        //    /// <summary>
        //    /// Selected year value for datepicker
        //    /// </summary>
        //    public int? SelectedYear { get; set; }

        //    /// <summary>
        //    /// Allowed file extensions for customer uploaded files
        //    /// </summary>
        //    public IList<string> AllowedFileExtensions { get; set; }

        //    public AttributeControlType AttributeControlType { get; set; }

        //    public IList<ProductAttributeValueModel> Values { get; set; }

        //}

        //public partial class ProductAttributeValueModel : BaseNopEntityModel
        //{
        //    public ProductAttributeValueModel()
        //    {
        //        PictureModel = new PictureModel();
        //    }

        //    public string Name { get; set; }

        //    public string ColorSquaresRgb { get; set; }

        //    public string PriceAdjustment { get; set; }

        //    public decimal PriceAdjustmentValue { get; set; }

        //    public bool IsPreSelected { get; set; }

        //    //picture model is used when we want to override a default product picture when some attribute is selected
        //    public PictureModel PictureModel { get; set; }
        //}
    }
}
