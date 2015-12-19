using FluentValidation.Attributes;
using Nop.Admin.Models.Catalog;
using Nop.Admin.Validators.Catalog;
using Nop.Plugin.Misc.GroupDeals.Enums;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupDeals.ViewModels
{
    [Validator(typeof(GroupDealValidator))]
    public class GroupDealViewModel : ProductModel
    {
        public List<SelectListItem> AvailableCountries { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.GroupDeals.Fields.CountryLabel")]
        public string Country { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.GroupDeals.Fields.StateOrProvinceLabel")]
        public string StateOrProvince { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.GroupDeals.Fields.CityLabel")]
        public string City { get; set; }
        public string CouponCode { get; set; }
        public int GroupdealStatusId { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.GroupDeals.Fields.FinePrintLabel")]
        public string FinePrint { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.GroupDeals.Fields.StatusLabel")]
        public GroupdealStatus GroupdealStatus
        {
            get { return (GroupdealStatus)this.GroupdealStatusId; }
            set { this.GroupdealStatusId = (int)value; }
        }
        public string GroupdealStatusName { get; set; }
        
        public GroupdealPictureViewModel GroupdealPictureViewModel { get; set; }   
    }

    public partial class GroupDealLocalizedModel : ProductLocalizedModel
    {

    }
}
