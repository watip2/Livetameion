using Nop.Plugin.Misc.GroupDeals.Views;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.GroupDeals
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute("Plugin.Misc.GroupDeals.Areas.Vendor.GroupDealsController",
                "Vendor/GroupDeals/{action}/{id}",
                new { area = "Vendor", controller = "GroupDeals", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.GroupDeals.Areas.Vendor.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.GroupDeals.Areas.Admin.GroupDealsController",
                "Admin/GroupDeals/{action}/{id}",
                new { area = "Admin", controller = "GroupDeals", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.GroupDeals.Areas.Admin.Controllers" }
            );//.DataTokens.Add("area", "Admin")
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.GroupDeals.Areas.Public.GroupDealsController",
                "GroupDeals/{action}/{id}",
                new { controller = "GroupDeals", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.GroupDeals.Areas.Public.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.GroupDeals.Areas.Public.GdShoppingCartController",
                "GdShoppingCart/{action}",
                new { controller = "GdShoppingCart", action = "Cart" },
                new[] { "Nop.Plugin.Misc.GroupDeals.Areas.Public.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            //add groupdeal to cart (without any attributes and options). used on catalog pages.
            route = routes.MapLocalizedRoute("AddGroupDealToCart-Catalog",
                            "addgroupdealtocart/catalog/{groupDealId}/{shoppingCartTypeId}/{quantity}",
                            new { controller = "GdShoppingCart", action = "AddGroupDealToCart_Catalog" },
                            new { groupDealId = @"\d+", shoppingCartTypeId = @"\d+", quantity = @"\d+" },
                            new[] { "Nop.Plugin.Misc.GroupDeals.Areas.Public.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);
            //add groupdeal to cart (with attributes and options). used on the groupdeal details pages.
            route = routes.MapLocalizedRoute("AddGroupDealToCart-Details",
                            "addgroupdealtocart/details/{groupDealId}/{shoppingCartTypeId}",
                            new { controller = "GdShoppingCart", action = "AddGroupDealToCart_Details" },
                            new { groupDealId = @"\d+", shoppingCartTypeId = @"\d+" },
                            new[] { "Nop.Plugin.Misc.GroupDeals.Areas.Public.Controllers" });
            routes.Remove(route);
            routes.Insert(0, route);

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
