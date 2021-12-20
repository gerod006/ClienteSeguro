using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Insurance
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
             // Configuración y servicios de API web
            config.MessageHandlers.Add(new ControlAccesoOpcion());
            // Rutas de API web
            // Formateo a JSON
            config.Formatters.JsonFormatter.SupportedMediaTypes
               .Add(new MediaTypeHeaderValue("text/html"));
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
             
            // Rutas de API web
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
