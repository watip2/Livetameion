using FluentValidation;
using Nop.Plugin.Misc.GroupDeals.ViewModels;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Models
{
    public class GroupDealValidator : BaseNopValidator<GroupDealViewModel>
    {
        public GroupDealValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(localizationService.GetResource("Plugins.Widgets.GroupDeals.Fields.Required"));

            RuleFor(x => x.ShortDescription)
                .NotEmpty().WithMessage(localizationService.GetResource("Plugins.Widgets.GroupDeals.Fields.Required"));

            RuleFor(x => x.AvailableStartDateTimeUtc)
                .NotEmpty().WithMessage(localizationService.GetResource("Plugins.Widgets.GroupDeals.Fields.Required"));
        }
    }
}
