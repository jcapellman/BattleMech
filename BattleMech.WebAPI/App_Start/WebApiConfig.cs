﻿using System.Web.Http;

namespace BattleMech.WebAPI {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            config.MessageHandlers.Add(new Handlers.Authandler());
            
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}