using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace PartB
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Clear();
            routes.MapPageRoute("images_mov", "images/{size}/{image}", "~/Image.aspx", false);
        }
    }
}
