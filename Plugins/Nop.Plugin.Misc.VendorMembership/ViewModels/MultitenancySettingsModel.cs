using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.ViewModels
{
    public class MultitenancySettingsModel : BaseNopModel
    {
        public MultitenancySettingsModel()
        {
            SalesCommissionSettings = new SalesCommissionSettingsModel();
        }
        public SalesCommissionSettingsModel SalesCommissionSettings { get; set; }

        #region Nested classes

        public partial class SalesCommissionSettingsModel : BaseNopModel
        {
            
        }
        #endregion
    }
}
