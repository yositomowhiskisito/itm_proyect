using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WSITM_Proyect
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(
                config.Formatters.XmlFormatter
                .SupportedMediaTypes
                .FirstOrDefault(t => t.MediaType == "application/xml"));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
