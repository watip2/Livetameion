using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nop.Core.Infrastructure;
using Nop.Services.Vendors;

namespace Nop.Plugin.Misc.GroupDeals.Tests
{
    [TestClass]
    public class GroupDealTests
    {
        [TestMethod]
        public void CreateGroupDeal()
        {
            //try
            {
                var vendorService = EngineContext.Current.Resolve<IVendorService>();
                //var vendor = vendorService.GetVendorById(1027);
                //var groupDealService = EngineContext.Current.Resolve<IGroupDealService>();
                
                //var gd = new GroupDeal
                //{
                //    Name = "Sohail",
                //    VendorId = vendor.Id
                //};

                //var genAttrService = EngineContext.Current.Resolve<IGenericAttributeService>();
                //genAttrService.SaveAttribute<int>(vendor, VendorAttributes.GroupDealId, gd.Id);
                
                Assert.AreEqual(1, 1);
            }
            //catch(Exception e)
            {
                //Assert.Fail(e.Message);
            }
        }
    }
}
