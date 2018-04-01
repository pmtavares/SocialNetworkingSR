using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SocialNetworkingSignalR
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Account", action="Index", id = UrlParameter.Optional }                
                );

            routes.MapRoute(
                "CreateAccount", 
                "Account/CreateAccount", 
                new {
                    controller = "Account",
                    action = "CreateAccount"
                }
            );

            routes.MapRoute(
                "Account", 
                "{username}", 
                new
                {
                    controller = "Account",
                    action = "Username"
                }
             );

            routes.MapRoute(
               "LoginPartial",
               "Account/LoginPartial",
               new
               {
                   controller = "Account",
                   action = "LoginPartial"
               }
            );

            routes.MapRoute(
                "Logout",
                "Account/Logout",
                new
                {
                    controller = "Account",
                    action = "Logout"
                }
             );
            routes.MapRoute(
                "Login",
                "Account/Login",
                new
                {
                    controller = "Account",
                    action = "LoginPartial"
                }
             );

            routes.MapRoute(
                "Profile",
                "Profile/{action}/{id}",
                new
                {
                    controller = "Profile",
                    action = "index",
                    id = UrlParameter.Optional
                }
             );

            routes.MapRoute(
                "AddFriend",
                "Profile/{action}",
                new
                {
                    controller = "Profile",
                    action = "AddFriend",
                    id = UrlParameter.Optional
                }
             );
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
